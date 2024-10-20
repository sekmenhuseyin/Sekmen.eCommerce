import client from "../../configs/axiosProduct"
const DEFAULT_QUERY = '/products'

export default class UserService {
  getAll = (params) => client.get(`${DEFAULT_QUERY}/`, { params })
  getById = id => client.get(`${DEFAULT_QUERY}/${id}`)
  addOrUpdate = model => model.id > 0 ? client.put(`${DEFAULT_QUERY}/`, model) : client.post(`${DEFAULT_QUERY}/`, model)
  delete = model => client.delete(`${DEFAULT_QUERY}/`, model)
}