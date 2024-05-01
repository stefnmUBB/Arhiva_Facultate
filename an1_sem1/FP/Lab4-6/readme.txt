----------------------------------------------------------------
                         The Problem
----------------------------------------------------------------

Creați o aplicație pentru gestiunea concurenților de la un concurs
de programare. Programul înregistrează scorul obținut de fiecare
concurent la 10 probe diferite, fiecare probă este notat cu un scor
de la 1 la 10. Fiecare participant este identificat printr-un număr
de concurs, scorul este ținut într-o listă unde concurentul 3 are
scorul pe poziția 3 în listă . Programul trebuie sa ofere următoarele
funcționalități:

1. Adaugă un scor la un participant.
    a) Adaugă scor pentru un nou participant (ultimul participant)
    b) Inserare scor pentru un participant.
2. Modificare scor.
    a) Șterge scorul pentru un participant dat.
    b) Șterge scorul pentru un interval de participanți.
    c) Înlocuiește scorul de la un participant.
3. Tipărește lista de participanți.
    a) Tipărește participanții care au scor mai mic decât un scor dat.
    b) Tipărește participanții ordonat după scor
    c) Tipărește participanții cu scor mai mare decât un scor dat, ordonat
       după scor.
4. Operații pe un subset de participanți.
    a) Calculează media scorurilor pentru un interval dat (ex. Se da 1 și
       5 se tipărește media scorurilor participanților 1,2,3,4 și 5
    b) Calculează scorul minim pentru un interval de participanți dat.
    c) Tipărește participanții dintr-un interval dat care au scorul multiplu de 10.
5. Filtrare.
    a) Filtrare participanți care au scorul multiplu unui număr dat. Ex. Se da
       numărul 10, se elimină scorul de la toți participanții care nu au scorul
       multiplu de 10.
    b) Filtrare participanți care au scorul mai mic decât un scor dat.
6. Undo
    • Reface ultima operație (lista de scoruri revine la numerele ce existau
      înainte de ultima operație care a modificat lista)


----------------------------------------------------------------
                          Iterations
----------------------------------------------------------------

Iteration 1 (Week 4):
  - create the main elements of the problem and write basic
    implementations for data manipulation features
    • target functionalities: 1ab, 2abc, 4abc, 5ab
    • target modules: participant.py
  - data visualization feature
    • target functionalities: 3abc
    • target modules: participant.py, table_printer.py
  - UI mockup
    • target modules: ui.py

Iteration 2 (Week 5):
  - undo feature implementation
    • target functionalities: 6
    • target modules: ...
  - UI polishing
    • target modules: ui.py

Iteration 3 (Week 6):
  - refactoring, bug fixes and optimization
    • target functionalities: all
    • target modules: all


----------------------------------------------------------------
                          Execution scenario
                          (subject to change)
----------------------------------------------------------------


             User               |        Program                     |     Description
                                |  (requests option)                 |
                              1 |                                    |
                                |  (requests suboption)              |
                              a |                                    |
                                |  (requests scores)                 |
(10,10,10,10,10,10,10,10,10,10) |                                    |
                                |  (requests options)                |
                              0 |                                    | load from dummy file (DEBUG ONLY)
                                |  (requests options)                |
                             5a |                                    |
                                |  (requests factor)                 |
                              6 |                                    |
                                | Filtered 3 participants out of 7.  |
                                | (requests options)                 |
                            3b  |                                    |
                                | Participants with scores 66 54 48  |


----------------------------------------------------------------
                           Activities
----------------------------------------------------------------

T1. Create a new Participant based on given code and scores.
  - target functionalities: 1a, 1b
  - points of interest: Participant

T2. Option to change the participant's score for a given problem.
  - target functionalities: 2c-1
  - points of interest: Participant.change_score

T3. Option to change the participant's score for all problems.
  - target functionalities: 2c-2
  - points of interest: Participant.change_scores

T4. Add a new participant to the participants list by their scores.
  - target functionalities: 1a
  - points of interest: ParticipantsList, ParticipantsList.add_participant_by_score

T5. Insert a new participant to the participants list by their code & scores.
  - target functionalities: 1b
  - points of interest: ParticipantsList.insert_participant

T6. Query a subset interval of the participants list
  - target functionalities: 4a, 4b
  - points of interest: ParticipantsList.average, ParticipantsList.min

T7. Filter participants based on a given condition.
  - target functionalities: 5a, 5b
  - points of interest: ParticipantsList.filter

T8. Print list of participants satisfying a given condition and in a given order
  - target functionalities: 3a, 3b, 3c, 4c
  - points of interest: ParticipantsList.print, TablePrinter

T9. UI implementation
    - points of interest: TablePrinter, ...