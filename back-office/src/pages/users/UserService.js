import client from "../../configs/axiosAuth"
const DEFAULT_QUERY = '/users'

export default class UserService {
  getAll = (params) => client.get(`${DEFAULT_QUERY}/`, { params })
  addOrUpdate = model => model.id > 0 ? client.put(`${DEFAULT_QUERY}/`, model) : client.post(`${DEFAULT_QUERY}/`, model)
  delete = model => client.delete(`${DEFAULT_QUERY}/`, model)
  updatePassword = (model) => client.put(`${DEFAULT_QUERY}/password`, model)
  getRole = (userId) => client.get(`${DEFAULT_QUERY}/role/${userId}`)
}