import client from "../../configs/axiosCoupon"
const DEFAULT_QUERY = '/coupons'

export default class UserService {
  getAll = (params) => client.get(`${DEFAULT_QUERY}/`, { params })
  getById = id => client.get(`${DEFAULT_QUERY}/${id}`)
  getByCode = code => client.get(`${DEFAULT_QUERY}/${code}`)
  addOrUpdate = model => model.id > 0 ? client.put(`${DEFAULT_QUERY}/`, model) : client.post(`${DEFAULT_QUERY}/`, model)
  delete = model => client.delete(`${DEFAULT_QUERY}/`, model)
}