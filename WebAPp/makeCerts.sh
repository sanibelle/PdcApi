mkdir certificates
cd certificates

# Install the local CA
../mkcert.exe -install

# Create certificates for localhost
../mkcert localhost