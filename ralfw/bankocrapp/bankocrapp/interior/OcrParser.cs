using System.Collections.Generic;
using bankocrapp.data;

namespace bankocrapp.interior
{
    /*
     * Aufbau eines OCR-Textes:
     * -OCR-Zeichen bestehen aus 3x3 ASCII-Zeichen (Leerzeichen, '_' und '|').
     * -Die ASCII-Zeichen sind auf 3 aufeinander folgende ASCII-Zeilen verteilt.
     * -OCR-Zeichen stehen direkt hintereinander.
     * -Auf die 3 zu einem OCR-Zeichen folgenden ASCII-Zeilen folgt eine ASCII-Leerzeile.
     * 
     *      aaa _  _  _  _  _     _ 
     *   |_|bbb| || ||_   |  |  ||_ 
     *     |ccc|_||_||_|  |  |  | _|
     *
     * Die Buchstaben stehen im OCR-Text an den für ein OCR-Zeichen relevanten ASCII-Positionen.
     *
     * -Die zu einem OCR-Zeichen gehörenden Positionen im ASCII-Text heißen 'Segmente'.
     * -Segmente sind von 0..8 von oben nach unten und links nach recht nummeriert.
     * -Von den 9 Segmenten sind nur 7 relevant, hier mit * bezeichnet:
     *
     *      a*a _  _  _  _  _     _ 
     *   |_|***| || ||_   |  |  ||_ 
     *     |***|_||_||_|  |  |  | _|
     *
     * -Ein ASCII-Text, der OCR-Daten enthält, wird in mehrere OcrLine zerlegt, die wiederum aus OcrChar bestehen.
     */
    internal static class OcrParser
    {
        public static IEnumerable<OcrLine> Parse(string[] lines) {
            for (var i = 0; i < lines.Length; i += 4) {
                yield return new OcrLine(lines[i], lines[i+1], lines[i+2]);
            }
        }
    }
}