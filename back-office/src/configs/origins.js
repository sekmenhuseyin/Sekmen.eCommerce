export const getOrigin = () => window.location.origin

export const getAuthOrigin = () => {
  return `${process.env.REACT_APP_AUTHAPI}/api`
}

export const getCouponOrigin = () => {
  return `${process.env.REACT_APP_COUPONAPI}/api`
}

export const getProductOrigin = () => {
  return `${process.env.REACT_APP_PRODUCTAPI}/api`
}

export const getEnvironmentName = () => {
  if (getOrigin().includes("localhost"))
    return "Local"
  if (getOrigin().includes("dev"))
    return "Development"
  if (getOrigin().includes("test"))
    return "Test"
  return "Production"
}