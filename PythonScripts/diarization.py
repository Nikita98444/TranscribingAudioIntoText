# -*- coding: utf-8 -*-
# diarization.py
from pyannote.audio import Pipeline
import sys
import os
import json

def speaker_diarization(input_wav_path):
    pipeline = Pipeline.from_pretrained("pyannote/speaker-diarization-3.1")
    diarization = pipeline(input_wav_path)
    
    # Подготовка результатов в виде списка словарей
    diarization_results = []
    for turn, _, speaker in diarization.itertracks(yield_label=True):
        diarization_results.append({
            'start': turn.start,
            'end': turn.end,
            'speaker': speaker
        })
    
    # Вывод результатов в формате JSON
    print(json.dumps(diarization_results))

if __name__ == "__main__":
    if len(sys.argv) != 2:
        print("Usage: python diarization.py <input_wav_path>")
        sys.exit(1)

    input_wav = sys.argv[1]

    if not os.path.exists(input_wav):
        print(f"Input file {input_wav} does not exist.")
        sys.exit(1)

    speaker_diarization(input_wav)
