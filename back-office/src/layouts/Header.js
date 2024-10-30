import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { Drawer, Layout, Menu } from 'antd'
import { DownOutlined, UserOutlined, LogoutOutlined, UnlockOutlined } from '@ant-design/icons'
import PasswordEdit from '../pages/users/PasswordEdit'
import useLocalStorage from '../utils/useLocalStorage'
import { getEnvironmentName } from '../configs/origins'

const { Header: AntHeader } = Layout

export default function Header() {
  const navigate = useNavigate()
  const [user] = useLocalStorage()
  const [drawerVisibility, setDrawerVisibility] = useState(false)

  const logout = () => {
    window.localStorage.clear();
    navigate("/login")
  }

  const profile = (
    <div className='ProfileName'>
      <div className='ProfileName-Users'>
        <UserOutlined style={{ fontSize: 18 }} /> {user.profile.name} <DownOutlined />
      </div>
      <div className='ProfileName-Desc'>Sekmen.Dev</div>
    </div>
  )

  return (
    <AntHeader
      className="site-layout-background"
      style={{ padding: 0, backgroundColor: 'white' }}
    >
      <div style={{ float: "left", paddingLeft: "10px", marginTop: "-13px" }}><h2>{getEnvironmentName()}</h2></div>
      <div className='ProfileMenu'>
        <Menu
          mode="horizontal"
          items={[
            {
              label: profile,
              key: 'profilemenu',
              children: [
                { label: 'Change Password', icon: <UnlockOutlined />, key: 'submenu-item-1', onClick: () => setDrawerVisibility(true) },
                { label: 'Logout', icon: <LogoutOutlined />, key: 'submenu-item-4', onClick: () => logout() }
              ]
            }
          ]} />
      </div>
      {drawerVisibility && <Drawer
        title="Change Password"
        width={400}
        open={drawerVisibility}
        onClose={() => { setDrawerVisibility(false) }}
      >
        <PasswordEdit onSuccess={() => { setDrawerVisibility(false) }} />
      </Drawer>}
    </AntHeader>
  )
}