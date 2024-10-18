export const getOrigin = () => window.location.origin

export const getWebsite = () => {
  return process.env.REACT_APP_API;
}

export const getServiceOrigin = () => {
  return `${process.env.REACT_APP_API}/api`
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