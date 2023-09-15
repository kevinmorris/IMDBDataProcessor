      ******************************************************************
      * Author:
      * Date:
      * Purpose:
      * Tectonics: cobc
      ******************************************************************
       IDENTIFICATION DIVISION.
       PROGRAM-ID. VS-IMPORT.

       ENVIRONMENT DIVISION.
       INPUT-OUTPUT SECTION.
       FILE-CONTROL.

           SELECT INFILE ASSIGN TO DINFILE.
           SELECT OUTFILE ASSIGN TO DOUTFILE
                          ORGANISATION IS INDEXED
                          ACCESS IS SEQUENTIAL
                          RECORD KEY IS OUT-KEY.

       DATA DIVISION.
       FILE SECTION.

       FD  INFILE.

       01 INFILE-RECORD-AREA          PIC X(163).

       FD  OUTFILE.

       01 OUTFILE-RECORD-AREA.
           05 FILLER                  PIC X(6).
           05 OUT-KEY                 PIC X(9).
           05 FILLER                  PIC X(148).


       WORKING-STORAGE SECTION.

       01  MASTER-RECORD              PIC X(163).

       01 SWITCHES.
           05 INFILE-EOF-SWITCH       PIC X    VALUE "N".

       PROCEDURE DIVISION.

       000-MAIN.


           STOP RUN.


       100-CREATE-RECORD.

           PERFORM 110-READ-INFILE-RECORD.
           IF NOT INFILE-EOF-SWITCH = "N"
               PERFORM 120-WRITE-OUTFILE-RECORD.

       110-READ-INFILE-RECORD.

           READ INFILE INTO MASTER-RECORD
               AT END MOVE "Y" TO INFILE-EOF-SWITCH.



       120-WRITE-OUTFILE-RECORD.

           WRITE OUTFILE-RECORD-AREA FROM MASTER-RECORD
               INVALID KEY
                   DISPLAY "WRITE ERROR: "
                           OUT-KEY
                   MOVE "Y" TO INFILE-EOF-SWITCH.



       END PROGRAM VS-IMPORT.
