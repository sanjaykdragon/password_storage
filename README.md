# password_storage

### table of contents
[description](#description)

[how it works](#how-it-works)

[disclaimer](#disclaimer)

[setup](#setup)

### description
simple encrypted password storage

### setup
step 1. modify test.php to your needs (change mysql server username, password, and dbname (and host if necessary).
step 2. setup local C# client (point to http://yoursite.com/)
step 3. done, DO NOT LOSE ENCRYPTION KEY, IT IS UNRECOVERABLE!

### how it works
All encryption / decryption is done client side, which prevents MITM hacks and database leaks.
However, this means that the users security is entirely in their hands. If they pick a bad password, their database can be decrypted. If their computer gets hacked, their passwords are gone. 

Encryption standard is AES-256 and HMAC. 
username, password, and site are encrypted and POST'ed to the PHP file (test.php), and the PHP file handles the request and does what is required (select from db, insert to db, etc).


### disclaimer

do not use this for serious purposes, everything is stored in plaintext as of 11/5/19, and communication between client and server is in plaintext

edit:

as of 11/6/19, all passwords are stored using AES then HMAC, and all the decryption is done on the client side. Client and server communications are still plaintext, but this shouldn't cause as much of a problem.

I am not responsible for damage / losses caused by failures of this program. Read the license.
