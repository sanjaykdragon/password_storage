# password_storage

simple password storage


do not use this for serious purposes, everything is stored in plaintext as of 11/5/19, and communication between client and server is in plaintext

edit:

as of 11/6/19, all passwords are stored using AES then HMAC, and all the decryption is done on the client side. Client and server communications are still plaintext, but this shouldn't cause as much of a problem.
