using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoundConverter.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vosk;

namespace SoundConverter.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public IConfiguration config;

        [ObservableProperty]
        private bool isModelEnable = true;

        [ObservableProperty]
        private bool isDropAllowed = true;

        [ObservableProperty]
        private Visibility crossMarkVisible = Visibility.Hidden;

        [ObservableProperty]
        private bool isButtonEnabled = true;

        [ObservableProperty]
        private Visibility checkMarkVisible = Visibility.Hidden;

        [ObservableProperty]
        private Visibility loadVisible = Visibility.Hidden;

        [ObservableProperty]
        private string borderBrushColor = "#C0C0C0"; // Изначально серый цвет рамки

        [ObservableProperty]
        private string droppedFileName = "Перетащите файл сюда"; // Изначальный текст

        [ObservableProperty]
        private bool isModelSelected = false;

        [ObservableProperty]
        private bool isAudioFile = false;

        /// <summary>
        /// Путь до аудио файла
        /// </summary>
        private string audioFilePath = "";
        /// <summary>
        /// Путь до модели AI
        /// </summary>
        private string aiModelFilePath = "";
        /// <summary>
        /// Временный файл WAV
        /// </summary>
        private string tempWavPath = "";
        /// <summary>
        /// Папка для выходных файлов
        /// </summary>
        private string outputPath = "";
        /// <summary>
        /// Питон скрипты
        /// </summary>
        private string pythonScriptsPath = "";

        /// <summary>
        /// Это поле будет использоваться для управления отменой текущей транскрипции.
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        private static readonly string[] SupportedAudioExtensions = { ".mp3", ".wav", ".flac", ".ogg", ".aac", ".wma" };

        [RelayCommand]
        private void HandleFileDrop(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                // Проверяем, является ли файл аудиофайлом
                var extension = Path.GetExtension(filePath).ToLower();
                if (SupportedAudioExtensions.Contains(extension))
                {
                    // Аудиофайл найден, устанавливаем цвет границы в зеленый и выводим название файла
                    BorderBrushColor = "Green";
                    DroppedFileName = Path.GetFileName(filePath);
                    audioFilePath = filePath;
                    IsAudioFile = true;
                }
                else
                {
                    // Неверный формат файла, устанавливаем цвет границы в красный и очищаем название файла
                    BorderBrushColor = "Red";
                    DroppedFileName = Path.GetFileName(filePath);

                    IsAudioFile = false;
                }
            }
        }

        [RelayCommand]
        private async Task HandleGetText()
        {
            try
            {
                bool isSuccess = true;

                LoadVisible = Visibility.Visible;
                IsButtonEnabled = false;
                CheckMarkVisible = Visibility.Hidden;
                CrossMarkVisible = Visibility.Hidden;

                IsDropAllowed = false; // Блокируем вставку нового файла

                IsModelEnable = false; // блокируем выбор модели

                // Выполнение транскрипции нейронкой на основе Vosk
                  isSuccess = await TranscribeAudioAsyncModel1(
                      audioFilePath,
                      tempWavPath,
                      aiModelFilePath,
                      outputPath
                      );



                if (isSuccess)
                    CheckMarkVisible = Visibility.Visible;
                else
                    CrossMarkVisible = Visibility.Visible;
            }
            finally
            {
                LoadVisible = Visibility.Hidden;
                IsButtonEnabled = true;
                IsDropAllowed = true;
                IsModelEnable = true;
            }
        }



        private async Task<bool> TranscribeAudioAsyncModel1(string audioFilePath, string tempWavFilePath, string modelDirectory, string outputTextFilePath)
        {
            return await Task.Run(() =>
            {
                bool success = true;

                try
                {
                    // Проверка наличия исходного аудиофайла
                    if (!File.Exists(audioFilePath))
                    {
                        throw new FileNotFoundException($"Файл {audioFilePath} не найден. Проверьте путь к файлу.");
                    }

                    Debug.WriteLine("Инициализация модели Vosk...");
                    using var model = new Model(modelDirectory);
                    Debug.WriteLine("Модель успешно инициализирована.");

                    Debug.WriteLine("Конвертация файла в WAV...");
                    var ffmpegPath = "ffmpeg";
                    var convertArgs = $"-y -i \"{audioFilePath}\" -ar 16000 -ac 1 \"{tempWavFilePath}\"";
                    var ffmpegProcess = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = ffmpegPath,
                            Arguments = convertArgs,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    };

                    ffmpegProcess.Start();
                    string ffmpegOutput = ffmpegProcess.StandardError.ReadToEnd();
                    ffmpegProcess.WaitForExit();

                    if (ffmpegProcess.ExitCode != 0)
                        throw new Exception($"Ошибка при конвертации аудиофайла: {ffmpegOutput}");

                    Debug.WriteLine("Конвертация завершена. Открытие WAV файла...");

                    // Вызов Python-скрипта и чтение вывода
                    Debug.WriteLine("Запуск Python-скрипта для диаризации...");
                    var pythonScriptPath = @"D:\PO\MyProjects\engine\Res\PythonScripts\diarization.py";

                    var pythonProcess = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "python",
                            Arguments = $"\"{pythonScriptPath}\" \"{tempWavFilePath}\"",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    };

                    pythonProcess.Start();
                    string pythonOutput = pythonProcess.StandardOutput.ReadToEnd();
                    string pythonError = pythonProcess.StandardError.ReadToEnd();
                    pythonProcess.WaitForExit();

                    if (pythonProcess.ExitCode != 0)
                        throw new Exception($"Ошибка при выполнении диаризации: {pythonError}");

                    Debug.WriteLine("Диаризация завершена. Результаты получены из stdout.");

                    // Парсинг JSON вывода
                    var diarizationSegments = JsonConvert.DeserializeObject<List<DiarizationSegment>>(pythonOutput);

                    if (diarizationSegments == null)
                        throw new Exception("Не удалось распарсить результаты диаризации.");

                    // Открытие WAV файла с помощью NAudio
                    using var waveReader = new WaveFileReader(tempWavFilePath);
                    var waveFormat = waveReader.WaveFormat;

                    if (waveFormat.Channels != 1 ||
                        waveFormat.BitsPerSample != 16 ||
                        !(waveFormat.SampleRate == 8000 ||
                          waveFormat.SampleRate == 16000 ||
                          waveFormat.SampleRate == 32000 ||
                          waveFormat.SampleRate == 44100))
                    {
                        throw new InvalidOperationException("Audio file must be WAV format mono PCM.");
                    }

                    using var recognizer = new VoskRecognizer(model, waveFormat.SampleRate);
                    recognizer.SetMaxAlternatives(0);
                    recognizer.SetWords(true);

                    Debug.WriteLine("Начало транскрипции...");

                    var resultTranscripts = new List<(double start, double end, string text)>();
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = waveReader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (recognizer.AcceptWaveform(buffer, bytesRead))
                        {
                            var result = JObject.Parse(recognizer.Result());
                            if (result["result"] != null)
                            {
                                foreach (var word in result["result"])
                                {
                                    if (word["start"] != null && word["end"] != null && word["word"] != null)
                                    {
                                        double start = (double)word["start"];
                                        double end = (double)word["end"];
                                        string textWord = word["word"].ToString();
                                        resultTranscripts.Add((start, end, textWord));
                                    }
                                }
                            }
                        }
                    }

                    var finalResult = JObject.Parse(recognizer.FinalResult());
                    if (finalResult["result"] != null)
                    {
                        foreach (var word in finalResult["result"])
                        {
                            if (word["start"] != null && word["end"] != null && word["word"] != null)
                            {
                                double start = (double)word["start"];
                                double end = (double)word["end"];
                                string textWord = word["word"].ToString();
                                resultTranscripts.Add((start, end, textWord));
                            }
                        }
                    }

                    // Создание сопоставления спикеров с номерами
                    var speakerMapping = new Dictionary<string, int>();
                    int speakerCounter = 0;
                    foreach (var speaker in diarizationSegments.Select(s => s.speaker).Distinct())
                    {
                        speakerMapping[speaker] = speakerCounter++;
                    }

                    // Создание списка слов с назначенными спикерами
                    var wordsWithSpeakers = new List<(int speakerNumber, double start, string text)>();

                    foreach (var word in resultTranscripts)
                    {
                        var matchingSegment = diarizationSegments.FirstOrDefault(s => word.end > s.start && word.start < s.end);

                        if (matchingSegment != null)
                        {
                            int speakerNumber = speakerMapping[matchingSegment.speaker];
                            wordsWithSpeakers.Add((speakerNumber, word.start, word.text));
                        }
                        else
                        {
                            wordsWithSpeakers.Add((-1, word.start, word.text));
                        }
                    }

                    // Сортировка слов по времени начала
                    wordsWithSpeakers = wordsWithSpeakers.OrderBy(w => w.start).ToList();

                    // Построение финальной транскрипции
                    StringBuilder speakerTranscript = new StringBuilder();

                    if (wordsWithSpeakers.Count > 0)
                    {
                        int currentSpeaker = wordsWithSpeakers[0].speakerNumber;
                        double currentStartTime = wordsWithSpeakers[0].start;
                        StringBuilder currentText = new StringBuilder(wordsWithSpeakers[0].text);

                        for (int i = 1; i < wordsWithSpeakers.Count; i++)
                        {
                            var word = wordsWithSpeakers[i];

                            if (word.speakerNumber == currentSpeaker)
                            {
                                // Продолжаем текст для того же спикера
                                currentText.Append(" " + word.text);
                            }
                            else
                            {
                                // Текущий спикер закончил говорить, обрабатываем его текст с переносом строк

                                string wrappedText = WrapText(currentText.ToString(), 170); // Перенос по 170 символов
                                var wrappedLines = wrappedText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                                foreach (var line in wrappedLines)
                                {
                                    TimeSpan startTimeSpan = TimeSpan.FromSeconds(currentStartTime);
                                    string timeString = startTimeSpan.ToString(@"hh\:mm\:ss");
                                    string speakerLabel = currentSpeaker >= 0 ? $"Speaker {currentSpeaker}" : "Unknown Speaker";

                                    speakerTranscript.AppendLine($"{speakerLabel}\t{timeString}\t{line}"); // Добавляем текст с новой строки
                                }

                                // Переходим к новому спикеру
                                currentSpeaker = word.speakerNumber;
                                currentStartTime = word.start;
                                currentText = new StringBuilder(word.text);
                            }
                        }

                        // Обрабатываем последнюю реплику последнего спикера
                        string lastWrappedText = WrapText(currentText.ToString(), 170);
                        var lastWrappedLines = lastWrappedText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var line in lastWrappedLines)
                        {
                            TimeSpan lastStartTimeSpan = TimeSpan.FromSeconds(currentStartTime);
                            string lastTimeString = lastStartTimeSpan.ToString(@"hh\:mm\:ss");
                            string lastSpeakerLabel = currentSpeaker >= 0 ? $"Speaker {currentSpeaker}" : "Unknown Speaker";

                            speakerTranscript.AppendLine($"{lastSpeakerLabel}\t{lastTimeString}\t{line.Trim()}"); // Добавляем текст последнего спикера
                        }
                    }


                    // **Добавляем обработку для удаления пустых строк**
                    var transcriptionText = speakerTranscript.ToString();
                    var cleanedLines = transcriptionText
                        .Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                        .Where(line => !string.IsNullOrWhiteSpace(line));
                    var cleanedTranscription = string.Join(Environment.NewLine, cleanedLines);

                    // Проверяем и создаем выходную директорию, если необходимо
                    if (!Directory.Exists(outputTextFilePath))
                    {
                        Directory.CreateDirectory(outputTextFilePath);
                    }

                    // Сохранение результата в файл с кодировкой UTF-8
                    var transcriptionFilePath = Path.Combine(outputTextFilePath, $"{DateTime.Now:yyyyMMddHHmmss}_transcription.txt");
                    File.WriteAllText(transcriptionFilePath, cleanedTranscription.ToString(), Encoding.UTF8);

                    Debug.WriteLine($"Транскрипция завершена. Результат сохранен в {transcriptionFilePath}");
                }
                catch (Exception ex)
                {
                    success = false;
                    Debug.WriteLine($"Ошибка в TranscribeAudioAsyncModel1: {ex.Message}");
                }
                finally
                {
                    // Удаление временного WAV-файла
                    if (File.Exists(tempWavFilePath))
                    {
                        try
                        {
                            File.Delete(tempWavFilePath);
                            Debug.WriteLine($"Временный файл {tempWavFilePath} удалён.");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Не удалось удалить временный файл {tempWavFilePath}: {ex.Message}");
                        }
                    }
                }

                return success;
            });
        }



        /// <summary>
        /// Метод открывает расположение файлов с текстом транскрибированным
        /// </summary>
        [RelayCommand]
        private void OpenOutFilePatch()
        {
            if (Directory.Exists(outputPath))
                OpenFolder(outputPath);
            else
                Console.WriteLine("Папка не найдена.");
        }

        static void OpenFolder(string path)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            };
            process.Start();
        }

        private string WrapText(string text, int maxLineLength)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var words = text.Split(' ');
            var wrappedText = new StringBuilder();
            var currentLine = new StringBuilder();

            foreach (var word in words)
            {
                if (currentLine.Length + word.Length + 1 > maxLineLength)
                {
                    if (currentLine.Length > 0)
                    {
                        wrappedText.AppendLine(currentLine.ToString());
                        currentLine.Clear();
                    }
                }

                if (currentLine.Length > 0)
                    currentLine.Append(' ');

                currentLine.Append(word);
            }

            if (currentLine.Length > 0)
                wrappedText.AppendLine(currentLine.ToString());

            return wrappedText.ToString();
        }


        public MainWindowViewModel()
        {
            // Проверяем, выполняется ли код в дизайнере Visual Studio
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return; // Не выполняем никакой код в дизайнере

            // Регистрация поставщика кодировок Windows
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            try
            {
                // Настройка конфигурации для чтения из appsettings.json
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                config = builder.Build();

                aiModelFilePath = config["AppSettings:AIModalPath1"].ToString();
                tempWavPath = config["AppSettings:TempWav"].ToString();
                outputPath = config["AppSettings:OutputFilePatch"].ToString();
                pythonScriptsPath = config["AppSettings:PythonScriptsPath"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MainWindowViewModel() -> {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }


    }
}
