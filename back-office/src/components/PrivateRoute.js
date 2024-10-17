import React from 'react'
import { Outlet, Navigate } from 'react-router-dom'
import { useLocalUser } from '../hooks/useLocalUser'

export default function PrivateRoute({ component: Component, ...props }) {
  const [user] = useLocalUser()

  if (user?.isAuthenticated)
      return <Outlet {...props} />

  return <Navigate to='/login' replace={true} />
}
