# PassGen 🔐

```
                                .                   ,;L.            
t                              ;W       ;W        .Gt       f#i EW:        ,ft
ED.                 ..        f#E      f#E       j#W:     .E#t  E##;       t#E
E#K:               ;W,      .E#f     .E#f      ;K#f      i#W,   E###t      t#E
E##W;             j##,     iWW;     iWW;     .G#D.      L#D.    E#fE#f     t#E
E# ##t           G###,    L##Lffi  L##Lffi  j#K;      :K#Wfff;  E#t D#G    t#E
E#  ##f        :E####,   tLLG##L  tLLG##L ,K#f   ,GD; i##WLLLLt E#t  f#E.  t#E
E#t ;##D.     ;W# G##,     ,W#i     ,W#i   j#Wi   E#t  .E#L     E#t   t#K: t#E
E#ELLE##K:   j#/  W##,    j#E.     j#E.     .G#D: E#t    f#E:   E#t    ;#W,t#E
E#L;;;;;;,  G##iHHG##,  .D#j     .D#j         ,K#fK#t     ,WW;  E#t     :K#D#E
E#t       :K#K:   L##, ,WK,     ,WK,            j###t      .D#; E#t      .E##E
E#t      ;##D.    L##, EG.      EG.              .G#t        tt ..         G#E
         ,,,      .,,  ,        ,                  ;;                       fE
```


**Created By:**
@Neevio-Source

A secure password generator which usses strong entropi 


## Table of Contents 

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Requirements](#requirements)
- [Installation](#installation)


## Overview

More detailed description of the project.

- Motivation / background

  I wanted to creat a password generator because I wanted to make sure its secure.
  I addet all the features I thougt were importend to include

- Goals of the project
  
  1. Make sure the password is purly random
  2. Making sure the password isn`t known

<br>


## Features

- Choosing Password length
- Choos wether or not using spezial Unicodes for the password
- Checking with [HIBP](https://haveibeenpwned.com/) if the password is known 

**Planed Feutures**

- Store the password in a local database

<br>

## Architecture

The application is a single-file, console-based password generator with a modular
functional architecture. While implemented in one class, the program is conceptually
split into clearly separated responsibilities.

--

### High-Level Overview

The program consists of four main conceptual layers:

1. User Interface (Console I/O)
2. Entropy & Password Generation
3. Network & External Validation
4. Utility & Cryptographic Functions

These layers interact linearly and do not share mutable global state.

<br>

--

### 1. User Interface Layer

Responsible for all user interaction via the console.

Includes:
- ASCII banner rendering
- User input handling (password length, unicode selection)
- Output formatting (password display, warnings, status messages)
- Program loop and control flow

This layer does not perform any cryptographic or network logic directly.

<br>
--


### 2. Entropy & Password Generation Layer

Responsible for generating cryptographically strong randomness and transforming it
into a user-readable password.

Key concepts:
- Multiple entropy sources (time-based data + system RNG)
- SHA-256 hashing and HMAC combination
- Deterministic expansion of entropy to arbitrary password lengths
- Mapping raw entropy bytes to a configurable character lexicon

The generator is designed to:
- Avoid predictable patterns
- Scale up to large password sizes
- Remain independent of user input quality

<br>

--

### 3. Network & External Validation Layer

Responsible for optional online validation of generated passwords.

Includes:
- Network availability check via ICMP (Ping)
- Integration with the Have I Been Pwned (HIBP) API
- k-Anonymity model (SHA-1 prefix querying)

Design goals:
- No plaintext password is ever transmitted
- Graceful degradation when offline
- Non-blocking behavior via async calls

This layer is optional and does not affect password generation itself.

<br>

--

### 4. Utility & Cryptographic Functions

Low-level helpers used by higher layers.

Includes:
- Cryptographic hash functions (SHA-1, SHA-256, HMAC)
- Secure random number generation
- Time-based entropy helpers
- Formatting and conversion utilities

These functions are stateless and reusable.

<br>

--

### Design Characteristics

- Single-process, single-threaded execution model
- Stateless password generation (no persistence)
- Defensive error handling with fallbacks
- Clear separation between generation and validation
- Console-first, platform-agnostic design

This architecture favors clarity, security, and predictability over abstraction
or framework complexity.

<br>

## Requirements

What is required to build or run the project?

- You need Linux or Windows

<br>

## Installation

**Windows**
  1. Download the ``Passgen_windows.exe`` file from the latest release.
  2. Start the programm by clicking the exe. Or Open Powershell navigate to the folder with the exe and execute
  ```bash
  start Passgen_windows.exe
  ```


**Linux**

  ``Binary``
  1. Download the ``Passgen_linux`` from the latest releases 
  2. Open your terminal and navigate to the folder with the binary then insert the following command 
  ```
  sudo chmod +x Passgen_linux
  ```
  3. Now you can execute the with
  ```bash
  ./Passgen_linux
  ```
--

  ``Deb``
  1. Download ``passgen_x.x.x_amd64.deb`` from the latest release 
  2. Install it with 
  ```
  sudo apt install ./passgen_x.x.x_amd64.deb
  ```
  3. Now you can execute the programm by typing ``passgen`` in your terminal

