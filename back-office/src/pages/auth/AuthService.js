import client from '../../configs/axios'
const DEFAULT_QUERY = '/auth'

export default class AuthService {
  login = (model) => client.post(`${DEFAULT_QUERY}/login`, model)
  register = (model) => client.post(`${DEFAULT_QUERY}/register`, model)
  forgotPassword = (model) => client.post(`${DEFAULT_QUERY}/forgot-password`, model)
}