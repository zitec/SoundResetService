
Sound Reset Service
=================

A lightweight .NET console application that keep the music rolling using browsers.

How does it work
----------------------

This program uses [NAudio](https://naudio.codeplex.com/) to pool the audio meter every 2 seconds. 
The application closes all the browsers if there is no audio signal above a cetain threshold (10%) in the last 60 seconds.
It will then open a random link that plays music (read from a file with music links) using the default browser (shell run).
