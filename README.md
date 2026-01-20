# PassGen 🔐

```
                             .                    ,;L.            
t                            ;W       ;W        .Gt       f#i EW:        ,ft
ED.               ..        f#E      f#E       j#W:     .E#t  E##;       t#E
E#K:             ;W,      .E#f     .E#f      ;K#f      i#W,   E###t      t#E
E##W;           j##,     iWW;     iWW;     .G#D.      L#D.    E#fE#f     t#E
E# ##t         G###,    L##Lffi  L##Lffi  j#K;      :K#Wfff;  E#t D#G    t#E
E#  ##f      :E####,   tLLG##L  tLLG##L ,K#f   ,GD; i##WLLLLt E#t  f#E.  t#E
E#t ;##D.   ;W#/G##,     ,W#i     ,W#i   j#Wi   E#t  .E#L     E#t   t#K: t#E
E#ELLE##K: j##/ W##,    j#E.     j#E.     .G#D: E#t    f#E:   E#t    ;#W,t#E
E#L;;;;;;,G##i,,G##,  .D#j     .D#j         ,K#fK#t     ,WW;  E#t     :K#D#E
E#t     :K#K:   L##, ,WK,     ,WK,            j###t      .D#; E#t      .E##E
E#t    ;##D.    L##, EG.      EG.              .G#t        tt ..         G#E
       ,,,      .,,  ,        ,                  ;;                       fE
```


**Created By:**
@Nevio_Pongiluppi

---

## Inhalt

1. [Idee / Nutzen](#idee--nutzen)  
2. [Programmbeschreibung und Ablauf](#programmbeschreibung-und-ablauf)  
3. [Screenshots](#screenshots)  
4. [Reflexion](#reflexion)  
5. [Code Review Mio Galli 10.01.2026](#code-review-mio-galli-10012026)  
6. [Funktionalität](#funktionalität)  
7. [Verständlichkeit](#verständlichkeit)  
8. [Stärken](#stärken)  
9. [Fazit](#fazit)
10. [Instalation](#instalation)
11. [Externe-Hilfe](#externe-hilfe)
    

---

## Idee / Nutzen

Die Idee für **PassGen** entstand, als mir bewusst wurde, dass ich selbst Passwort-Generatoren nutze, ohne wirklich zu wissen, was dahinter passiert. Das ließ mich unwohl fühlen, also entschloss ich mich, einen eigenen zu bauen, dem ich vertrauen kann.

**PassGen** ist ein CLI-Tool zum Erstellen von **nicht vorhersehbaren und nicht geleakten Passwörtern**.

- Passwortlänge: 0–200 Zeichen  
- Nach der Erstellung wird mit Hilfe von **HIBP (Have I Been Pwned)** überprüft, ob das Passwort bereits in einem Datenleck gefunden wurde.

---

## Programmbeschreibung und Ablauf

### Start und Initialisierung

- Konsolentitel wird gesetzt  
- Großes Zeichenalphabet wird definiert:  
  - Groß- und Kleinbuchstaben  
  - Zahlen  
  - Sonderzeichen  
  - Unicode-Symbole  
  - Umlaute  
- Dadurch hohe Entropie und Sicherheit der generierten Passwörter

### Banner Ausgabe

- ASCII-Banner zu Beginn jedes Durchlaufs  
- Reine optische Darstellung, ohne Einfluss auf die Programmlogik

### Benutzereingabe der Passwortlänge

- Benutzer wird aufgefordert, Passwortlänge einzugeben  
- Standardwert: 20 Zeichen, wenn Eingabe ungültig oder leer  
- Negative Werte → Standardwert  
- Werte >200 → auf 200 begrenzt  
- Easter Egg möglich (keine schädliche Überraschung, bleibt geheim)

### Erzeugung der Zufallsbasis

- SHA-256 Hash wird erzeugt aus:  
  1. Externer API (Webcam-Input, live, selbst gehostet)  
  2. Lokale kryptographisch sichere Zufallsdaten  
- Kombination der beiden Hashes via **HMAC SHA-256**  
- Fallback: rein lokale Zufallsdaten, falls API nicht erreichbar

### Aufbau des Passworts

- Hash → Byte-Array  
- Modulo-Operation auf Alphabet → Passwortzeichen  
- Gleichmäßig verteiltes, zufälliges Passwort

### Ausgabe des Passworts

- Passwort wird in Konsole farblich hervorgehoben

### Überprüfung auf Datenlecks

- SHA-1 Hash → HIBP API (K-Anonymity)  
- Benutzer wird gewarnt, falls Passwort geleakt  
- Positive Rückmeldung, wenn Passwort sicher

### Wiederholung des Programms

- Nach Tastendruck: neuer Ablauf, neues Passwort kann generiert werden

---

## Screenshots

**Programm bei Start:**  
![Start](screenshots/start.png)

**Wenn Enter ohne Eingabe gedrückt:**  
![Enter ohne Eingabe](screenshots/enter.png)

**Länge 2 gewählt, Passwort in Datenbank gefunden:**  
![Länge 2](screenshots/length2.png)

**Minuszahl eingegeben:**  
![Minuszahl](screenshots/minus.png)

**Über 200 eingegeben:**  
![Über 200](screenshots/over200.png)

*(Screenshots bitte durch tatsächliche Bilder ersetzen)*

---

## Reflexion

Ich bin persönlich zufrieden mit dem bisher Geschaffenen:

- Sinnvolles Programm zur Erstellung sicherer Passwörter  
- Besonders gelungen: HIBP-Funktion  

Schwierigkeiten:

- Einbindung von APIs in C# war zunächst herausfordernd  
- Nächstes Mal: GUI für 0815-Nutzer planen  
- Möglichkeit für Nutzer-Feedback via GitHub, um Funktionen einzubinden

---

## Code Review Mio Galli 10.01.2026

### Funktionalität

- Läuft fehlerfrei  
- Erfüllt alle Aufgaben  
- Nutzerinteraktionen gut gestaltet und verständlich

### Verständlichkeit

- Gut lesbarer und strukturierter Code  
- Sinnvolle Variablen- und Funktionsnamen  
- Kommentare passend platziert

### Stärken

- Gut gewählte Variablennamen  
- Saubere Struktur  
- Starkes Error Handling

### Schwächen

- Rechtschreibfehler im Code  
- Keine Möglichkeit, zwischen normalen und Sonder-Unicodes zu wählen

---

## Fazit

- Code übertrifft die Anforderungen  
- Gute Fehlerbehandlung  
- Nutzung von APIs, Hashing und HMAC  
- Strukturierter und gut lesbarer Code  

Insgesamt zeigt der Code ein gutes Verständnis für saubere Programmierung und sicheren Umgang mit den verwendeten Technologien.

---

## Lizenz & Attribution

**MIT-Lizenz (Attribution erforderlich)**  

Du darfst **PassGen** frei verwenden, modifizieren und weitergeben, **solange Nevio als ursprünglicher Autor erwähnt wird**.

```text
Copyright (c) 2026 Nevio

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to use, copy,
modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
provided that the above copyright notice and this permission notice are included
in all copies or substantial portions of the Software.
```
---

## Instalation
**Windows**
1. Gehen sie auf Release
2. Laden sie sich die neuste version runter ```Passgen-windows.exe```
3.Starten sie dass Programm durch Doppel-Klick auf ```Passgen-windows.exe```

**Linux**
1. Gehen sie auf Release
2. Laden sie sich dass neuste binary runter ```Passgen-linux-binary```
3. Navigieren sie zum Donwload Ordner
4. Öfnnen sie dass terminal und geben sie mit diesem befehl ``` sudo chmod+x Passgen-linux-binary ``` die nötigen berechtigungen
5. Starten sie dass Programm mit ```./Passgen-linux-binary```
---
##Externe-Hilfe
Beim Erstellen des Codes habe ich folgende Hilfen genutzt:
- Online‑Dokumentation zur HIBP‑API und SHA‑Hash‑Methoden
- StackOverflow zur Fehlersuche bei API‑Anfragen
- ChatGPT für Textformulierungen und Strukturierung des READMEs

Ich habe **keine KI‑generierten Code‑Blöcke verwendet**, sondern nur zur **Sprachhilfe** und zur Erklärung von Konzepten.




