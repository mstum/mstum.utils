.net Utility Library
====================

Just some Misc. Utilities that I wrote over time.

Base36 Encoder/Decoder
----------------------
Convert an Int64 or BigInteger to a Base36 string and vice versa.
http://www.stum.de/2008/10/20/base36-encoderdecoder-in-c/
http://en.wikipedia.org/wiki/Base36

.INI File Parser
----------------
Read and Write .ini Files

http://www.stum.de/2009/08/15/a-simple-ini-file-parser-for-c/

A simple Even/Odd Cycler
------------------------
Useful when writing out a table and switching css classes for each row

http://www.stum.de/2010/01/22/a-simple-evenodd-cycler-for-net/

A Command Line Parser
---------------------
There are many Command Line Argument Parsers, some simple, some complicated. Here is mine.

http://www.stum.de/2008/06/22/parsing-the-command-line/

Extension method to replace multiple Chars in a string
------------------------------------------------------
Replace characters in a string with another string or remove them altogether.

http://www.stum.de/2010/02/16/an-extension-method-to-replace-multiple-chars-in-a-string/

Return another string if string is null or empty
------------------------------------------------
    var displayString = stringThatMaybeNull.IfEmpty("None set.");

http://www.stum.de/2010/01/16/extension-method-return-another-string-if-string-is-null-or-empty/

String.Format Extension method
------------------------------
    "Hello, {0}".Use("Michael")

http://www.stum.de/2009/08/20/turning-string-format-into-an-extension-method/


License
-------

A copy of the license can be found in LICENSE.txt or at http://mstum.mit-license.org/