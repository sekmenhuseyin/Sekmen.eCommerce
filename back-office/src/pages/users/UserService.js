import client from '../../configs/axios'
const DEFAULT_QUERY = '/users'

export default class UserService {
  getAll = (params) => client.get(`${DEFAULT_QUERY}/`, { params })
  addOrUpdate = model => model.id > 0 ? client.put(`${DEFAULT_QUERY}/`, model) : client.post(`${DEFAULT_QUERY}/`, model)
  delete = model => client.delete(`${DEFAULT_QUERY}/`, model)
  updatePassword = (model) => client.put(`${DEFAULT_QUERY}/password`, model)
  detail = (userId) => client.get(`${DEFAULT_QUERY}/${userId}/detail`)
  getRoles = () => client.get(`${DEFAULT_QUERY}/roles`)
  getPasswordPolicy = () => client.get(`${DEFAULT_QUERY}/password-policy`)
}