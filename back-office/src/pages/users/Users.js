import React, { useEffect, useState } from 'react'
import { Button, Drawer, message, Table } from 'antd'
import { PlusOutlined, UserOutlined } from '@ant-design/icons'
import Page from '../../components/Page'
import userColumns from './user-columns'
import UserService from './UserService'
import UserEdit from './UserEdit'

const userService = new UserService()

export default function Users() {
  const [ready, setReady] = useState(false)
  const [data, setData] = useState()
  const [current, setCurrent] = useState()
  const [drawerVisibility, setDrawerVisibility] = useState(false)
  const [passwordPolicy, setPasswordPolicy] = useState()

  useEffect(() => {
    userService.getPasswordPolicy().then((x) => {
      setPasswordPolicy(x.data)
    }).catch(() => { })
    load()
  }, [])

  function load() {
    setReady(false)
    userService.getAll()
      .then(m => setData(m.data))
      .catch(() => { message.error("Unexpected error") })
      .finally(() => { setReady(true) })
  }

  function add() {
    setCurrent(null)
    setDrawerVisibility(true)
  }

  async function edit(row) {
    setCurrent(row)
    setDrawerVisibility(true)
  }

  async function remove(row) {
    setReady(false)
    await userService.delete(row.id)
    message.success(`${row.name} is deleted`)
    load()
  }

  function success() {
    setDrawerVisibility(false)
    load()
  }

  return (
    <Page
      title="Users"
      routes={[{ href: '/users', name: 'Users', icon: <UserOutlined /> }]}
      extra={[
        <Button key="add" type="primary" onClick={add}>
          <PlusOutlined /> Add user
        </Button>
      ]}
    >
      <Table
        rowKey="id"
        scroll={{ x: 1000 }}
        dataSource={data}
        columns={userColumns(edit, remove)}
        onRow={(record, _) => {
          return {
            onDoubleClick: (_) => { edit(record) }
          }
        }}
        loading={!ready}
        pagination={null}
      />
      <Drawer
        open={drawerVisibility}
        onClose={() => { setDrawerVisibility(false); }}
        width={500}
        styles={{ body: { paddingTop: 20 } }}
        title={current ? 'Edit User' : 'Add User'}
      >
        <UserEdit onSuccess={success} model={current} passwordPolicy={passwordPolicy} />
      </Drawer>
    </Page>
  )
}
