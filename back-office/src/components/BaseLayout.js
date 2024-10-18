import { useEffect, useState } from 'react'
import { Link, useLocation, Outlet } from 'react-router-dom';
import { Layout, Menu } from 'antd'
import { UserOutlined } from '@ant-design/icons'
import Header from './Header'
import Footer from './Footer'

const { Content, Sider } = Layout

export default function BaseLayout() {
  const location = useLocation();
  const [collapsed, setCollapsed] = useState(false)
  const [selectedKey, setSelectedKey] = useState('1')
  const [openKeys, setOpenKeys] = useState()

  useEffect(() => {
    const urlSearchParams = new URLSearchParams(location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    if (params.menu) {
      setOpenKeys([params.menu]);
    }
    if (params.subMenu) {
      setSelectedKey(params.subMenu);
    }
  }, [location])

  function onOpenChange(keys) {
    let rootSubmenuKeys = menuItems.map(m => m.key)
    let latestOpenKey = keys.find(key => openKeys.indexOf(key) === -1);
    if (rootSubmenuKeys.indexOf(latestOpenKey) === -1) {
      setOpenKeys(keys);
    } else {
      setOpenKeys(latestOpenKey ? [latestOpenKey] : []);
    }
  }

  const menuItems = [
    { label: <Link className='MenuLinks' to="/coupons">Kuponlar</Link>, icon: <UserOutlined />, key: 'coupons' },
    { label: <Link className='MenuLinks' to="/users">Kullanıcılar</Link>, icon: <UserOutlined />, key: 'users' }
  ]

  return (
      <Layout style={{ minHeight: '100vh' }}>
        <Sider className='BaseLayout-Aside' collapsible collapsed={collapsed} onCollapse={setCollapsed}>
          <div className='BaseLayout-LogoContainer'>
            <Link className='MenuLinks' to="/">
              Office
            </Link>
          </div>
          <Menu
            items={menuItems}
            theme="dark"
            selectedKeys={[selectedKey]}
            openKeys={openKeys}
            mode="inline"
            onClick={({ key }) => setSelectedKey(key)}
            onOpenChange={onOpenChange}
          />
        </Sider>
        <Layout>
          <Header />
          <Content>
            <Outlet />
          </Content>
          <Footer />
        </Layout>
      </Layout>
  )
}