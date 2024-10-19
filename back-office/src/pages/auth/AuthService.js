import client from '../../configs/axios'
const DEFAULT_QUERY = '/auth'

export default class AuthService {
  login = (model) => client.post(`${DEFAULT_QUERY}/login`, model, {
    headers: { 'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8' }
  })
  register = (model) => client.post(`${DEFAULT_QUERY}/register`, model, {
    headers: { 'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8' }
  })
  forgotPassword = (model) => client.post(`${DEFAULT_QUERY}/forgot-password`, model)
  getPasswordPolicy = () => client.get(`${DEFAULT_QUERY}/password-policy`)
}