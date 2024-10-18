
export function createRandomPassword(passwordPolicy) {
    var password = generatePassword(passwordPolicy.requiredLength);
    while (!validatePassword(password, passwordPolicy)) {
        password = generatePassword(passwordPolicy.requiredLength);
    }
    return password;
}

export function PasswordCharacterCount(password) {
    var upperCaseCount = password.match(/[A-Z]/g)?.length ?? 0;
    var lowerCaseCount = password.match(/[a-z]/g)?.length ?? 0;
    var numberCount = password.match(/\d/g)?.length ?? 0;
    var specialCharacterCount = password.match(/[!'^+%&/()=?_\-<>|.:,;@"]/g)?.length ?? 0;
    var length = password.length;
    return { upperCaseCount, lowerCaseCount, numberCount, specialCharacterCount, length };
}

function generatePassword(length) {
    var password = "";
    for (var i = 0; i < length * 2; i++) {
        var randomNumber = Math.floor(Math.random() * 94) + 33;
        password += String.fromCharCode(randomNumber);
    }
    return password;
}

function validatePassword(password, passwordPolicy) {
    var { upperCaseCount, lowerCaseCount, numberCount, specialCharacterCount } = PasswordCharacterCount(password);

    if (password.length < passwordPolicy.requiredLength) {
        return false;
    }

    if (passwordPolicy.requireUppercase && upperCaseCount <= passwordPolicy.requiredUniqueChars) {
        return false;
    }

    if (passwordPolicy.requireLowercase && lowerCaseCount <= passwordPolicy.requiredUniqueChars) {
        return false;
    }

    if (passwordPolicy.requireDigit && numberCount <= passwordPolicy.requiredUniqueChars) {
        return false;
    }

    if (passwordPolicy.requireNonAlphanumeric && specialCharacterCount <= passwordPolicy.requiredUniqueChars) {
        return false;
    }

    return true;
}
