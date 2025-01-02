# Stringspector

A little tool that analyzes a given string in multiple ways.

* general inspections
    * what's the length of the string?
    * is it trimmable? If so, what is its length? And which characters are trimmed away?
    * how many lines do we have?
    * how many white-space characters do we have?
    * are there any non-ASCII characters?
* single character inspections
    * what's the ASCII-int-value? or ASCII-hex-value?
    * for non-ASCII characters: the UTF8 byte count?
* number inspections
    * what is the hex value of it?
    * what is the ASCII character?
    * what's the time, if the number represents milliseconds since unix epoch?
* list inspections
    * what's the most likely separator?
    * how many items do we have in total?
    * how many distinct items do we have?
    * if they are all numbers, what's min/avg/max/sum?
* magic numbers
    * some time relevant values like 1440, 3600, 86400
    * min/max values of datatypes like int16, uint32
    * what's the text for HTTP status code 418?
* encodings
    * is it a valid Base64 string? What is it's decoded text?
    * what is the text if I HTML-decode it?
    * what is the text if I URL-decode it?
