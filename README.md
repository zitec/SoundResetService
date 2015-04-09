
Sound Reset Service
=================

A lightweight .NET console application that keep the music rolling.

How does it work
----------------------

This program uses [NAudio](https://naudio.codeplex.com/) to pool the audio meter every 2 seconds. 
After if 60 seconds pass and the audio meter did pass a certain threshold (10%), it will kill all the browsers. 
It will then open a random link that plays music (read from a file with music links) using the default browser (shell run).
