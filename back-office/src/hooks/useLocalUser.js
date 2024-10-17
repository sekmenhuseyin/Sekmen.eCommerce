import { jwtDecode } from "jwt-decode";
import { useLocalStorage } from "./useLocalStorage";

export function useLocalUser() {
    const [user, setUser] = useLocalStorage("user")
    const getValue = () =>{
        if(!user?.access_token)
          return null

        let jwt = jwtDecode(user.access_token)
        const isAuthenticated = jwt && jwt?.exp >= Date.now() / 1000
        const isAdmin = isAuthenticated && (jwt?.role === "admin" || (Array.isArray(jwt.role) && jwt?.role.filter(x => x === "admin")?.length > 0))
        return {
            ...user,
            isAuthenticated,
            isAdmin
        }
    }
    return [getValue(), setUser]
}