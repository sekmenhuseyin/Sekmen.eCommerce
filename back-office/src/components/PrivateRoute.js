import React from 'react'
import { Outlet, Navigate } from 'react-router-dom'
import useLocalStorage from '../utils/useLocalStorage'

export default function PrivateRoute({ component: Component, ...props }) {
  const [user] = useLocalStorage()

  if (user?.profile?.isAuthenticated)
    return <Outlet {...props} />

  return <Navigate to='/login' replace={true} />
}
