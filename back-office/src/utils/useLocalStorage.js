import { jwtDecode } from "jwt-decode";
import { useState, useEffect } from "react";

const useLocalStorage = () => {
  const key = "token"
  const [value, setValue] = useState(() => {
    let currentValue;

    try {
      currentValue = JSON.parse(
        localStorage.getItem(key) || String(null)
      );
    } catch (error) {
      currentValue = null;
    }

    if (!currentValue)
        return currentValue
    
    let profile = jwtDecode(currentValue.access_token)
    profile.isAuthenticated = profile && profile?.exp >= Date.now() / 1000

    return {...currentValue, profile};
  });

  useEffect(() => {
    localStorage.setItem(key, JSON.stringify(value));
  }, [value, key]);

  return [value, setValue];
};

export default useLocalStorage;