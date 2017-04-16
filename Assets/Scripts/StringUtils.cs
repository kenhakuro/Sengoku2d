﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringUtils : MonoBehaviour {

    private const string PASSWORD_CHARS =
        "0123456789abcdefghijklmnopqrstuvwxyz";

    public static string GeneratePassword(int length) {
        var sb = new System.Text.StringBuilder(length);
        var r = new System.Random();

        for (int i = 0; i < length; i++) {
            int pos = r.Next(PASSWORD_CHARS.Length);
            char c = PASSWORD_CHARS[pos];
            sb.Append(c);
        }

        return sb.ToString();
    }
}
