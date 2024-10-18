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

  async me() {
    const token = JSON.parse(localStorage.getItem('user'))?.access_token
    let req = await client.get(`${DEFAULT_QUERY}/me`, {
      headers: { Authorization: `Bearer ${token}` },
    })
    let _model = {
      access_token: token,
      profile: {
        name: req.data.fullname,
        firstName: req.data.firstName,
        lastName: req.data.lastName,
        companyName: req.data.companyName,
        companyId: req.data.companyId,
      },
      permissions: req.data.permissions,
    }

    localStorage.setItem('user', JSON.stringify(_model))
    return { ..._model, mustChangePassword: req.data.mustChangePassword }
  }
}