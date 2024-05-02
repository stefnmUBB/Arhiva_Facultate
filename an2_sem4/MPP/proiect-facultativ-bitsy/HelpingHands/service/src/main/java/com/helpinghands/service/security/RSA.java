package com.helpinghands.service.security;

import org.bouncycastle.jce.provider.BouncyCastleProvider;

import javax.crypto.BadPaddingException;
import javax.crypto.Cipher;
import javax.crypto.IllegalBlockSizeException;
import javax.crypto.NoSuchPaddingException;
import java.nio.charset.StandardCharsets;
import java.security.*;
import java.security.spec.PKCS8EncodedKeySpec;
import java.security.spec.RSAPrivateKeySpec;
import java.security.spec.X509EncodedKeySpec;
import java.util.Base64;

public class RSA {
    public static PublicKey getPublicKey(String key){
        try{
            byte[] byteKey = Base64.getDecoder().decode(key
                    .replace("\n","")
                    .replace("-----BEGIN PUBLIC KEY-----","")
                    .replace("-----END PUBLIC KEY-----","")
                    .getBytes());
            var pkSpec = new X509EncodedKeySpec(byteKey);
            var kf =  KeyFactory.getInstance("RSA");
            return kf.generatePublic(pkSpec);
        }
        catch(Exception e){
            throw new RuntimeException(e);
        }
    }

    public static PrivateKey getPrivateKey(String key){
        try{
            byte[] byteKey = Base64.getDecoder().decode(key
                    .replace("-----BEGIN PRIVATE KEY-----","")
                    .replace("-----END PRIVATE KEY-----","")
                    .replaceAll("\\s+","")
                    .getBytes());
            var pkSpec = new PKCS8EncodedKeySpec(byteKey);

            var kf = KeyFactory.getInstance("RSA");
            return kf.generatePrivate(pkSpec);
        }
        catch(Exception e){
            throw new RuntimeException(e);
        }
    }

    private static final String ENCRYPTION_ALGORITHM="RSA/ECB/OAEPWithSHA-1AndMGF1Padding";

    public static String encode(String message, PublicKey key){
        var bytes = message.getBytes(StandardCharsets.UTF_8);
        try {
            var encryptCipher = Cipher.getInstance(ENCRYPTION_ALGORITHM);
            encryptCipher.init(Cipher.ENCRYPT_MODE, key);
            var encrypted = encryptCipher.doFinal(bytes);
            return Base64.getEncoder().encodeToString(encrypted);
        } catch (Exception e) {
            throw new RuntimeException(e);
        }

    }

    public static String encode(String message, String publicKey) {
        return encode(message, getPublicKey(publicKey));
    }

    public static String decode(String crypt64, PrivateKey key){
        var bytes=Base64.getDecoder().decode(crypt64);
        try {
            var encryptCipher = Cipher.getInstance(ENCRYPTION_ALGORITHM);
            encryptCipher.init(Cipher.DECRYPT_MODE, key);
            var decrypted = encryptCipher.doFinal(bytes);
            return new String(decrypted, StandardCharsets.UTF_8);
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }
    public static String decode(String crypt64, String privateKey) {
        return decode(crypt64, getPrivateKey(privateKey));
    }
}
