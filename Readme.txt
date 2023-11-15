TravelPal
TravelPal är en enkel WPF-app där man som användare kan lägga till, ta bort, överskåda och, till viss mån ändra i sina resor.
I varje resa finns bl.a. val så som packlista, antal resenärer och vad syftet med resan är.
Man kan logga in som administratör och överskåda, ändra och ta bort andra användares resor.



Vad har jag gjort annorlunda?

Lagt till att passport inte är required om användaren och destinationen är i samma land. Oavsett om det är innanför eu eller ej. 

Använde user i constructorn för varje fönster istället för signedinuser

Två olika construktorer i mainwindow för att inte travels ska läggas till varje gång till den bestämda användaren. Detta var en av de tidigaste sakerna jag gjorde, hae jag återkommit till detta senare i projektet hade jag antagligen gjort det annorlunda.

Bestämde mig för att inte ha med user details window.

Valt att inte lägga till så man kan ändra travel days eller luggage i travel details då detta skulle kladda till UIn extremt mycket.


Vad borde jag gjort annorlunda?

Använt guard clauses istället för så långa if/else-kedjor för att öka läsbarhet och göra enklare att debugga.

Satt user som property på varje travel för att underlätta hanterandet av travels som admin och minska antalet loopar.

Jag förstod först inte värdet av signedInUser propertien, men hade jag gjort om projektet nu hade jag använt det istället. Det hade underlättat skrivandet av koden.


