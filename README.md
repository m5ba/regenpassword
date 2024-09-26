# How This Tool Works and Why It’s Secure

## Why

This app is designed to solve the common problem of password reuse across multiple resources while maintaining ease of use. It allows users to remember **one secure secret** and generate unique passwords for each resource they access. The app ensures that no two passwords are the same, even though they are all generated from the same secret.

The password generation is **deterministic**, meaning that the same combination of **resource name**, **login**, **secret**, and **iteration** will always result in the same password. This enables you to easily regenerate your password without needing to store or remember it, as long as you can recall the original inputs.

Crucially, a **single change** in any of the input parameters—whether the resource, login, secret, or iteration—will lead to a completely different password. This ensures that even if someone were to access one of your generated passwords, they would not be able to infer your password for other resources, effectively preventing the risks associated with password reuse.

## How

The app generates a unique password by combining several user inputs: **resource name**, **login**, **secret**, and **iteration**. These inputs are concatenated into a single string, which serves as the basis for the password generation process.

This concatenated string is then passed through a **SHA-512 hashing algorithm**, which produces a fixed-size 512-bit (64-byte) hash. SHA-512 is a cryptographic hash function that converts the input into a secure, irreversible sequence of bytes.

The resulting hash is processed byte-by-byte to create the final password. The first byte of the hash determines the length of the password, which will be between 16 and 20 characters. Special positions in the password are selected based on the next few bytes of the hash, and at these positions, special symbols from a predefined set are inserted. For all other positions, characters are chosen from a URL-safe **Base64 character set**.

The final output is a secure, deterministic password that can be regenerated using the same set of inputs. A single change to any input (resource name, login, secret, or iteration) will result in a completely different password, ensuring high security and uniqueness for each resource.

## Security and Privacy

All user input is processed locally within your browser. No data is stored, transmitted, or sent to any external server. Your information remains entirely private and secure on your device.
