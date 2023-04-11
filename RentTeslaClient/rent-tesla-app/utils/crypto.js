import CryptoJS from 'crypto-js';

export function encryptObject(obj) {
  const ciphertext = CryptoJS.AES.encrypt(JSON.stringify(obj), process.env.SECRET_KEY).toString();
  return ciphertext;
}
export function decryptObject(ciphertext) {
    const bytes = CryptoJS.AES.decrypt(ciphertext, process.env.SECRET_KEY);
    const plaintext = bytes.toString(CryptoJS.enc.Utf8);
    const obj = JSON.parse(plaintext);
    return obj;
  }
  