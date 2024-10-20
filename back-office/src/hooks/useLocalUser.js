import { jwtDecode } from "jwt-decode";
import { useLocalStorage } from "./useLocalStorage";

export function useLocalUser() {
    const [token, setToken] = useLocalStorage("token")
    const getValue = () => {
        if (!token)
            return null

        let jwt = jwtDecode(token)
        jwt.isAuthenticated = jwt && jwt?.exp >= Date.now() / 1000
        return jwt
    }
    return [getValue(), setToken]
}