import React, { useEffect, useState } from 'react'
import { Form, Row, Col, Input, Button, message, Divider, Checkbox, Switch } from 'antd'
import { CloseOutlined, CheckOutlined } from '@ant-design/icons'
import UserService from './UserService'
import { Password } from '../../components/Password'

const userService = new UserService()

export default function UserEdit({ onSuccess, model, passwordPolicy = {} }) {
  const [formRef] = Form.useForm()
  const [ready, setReady] = useState(true)
  const [roles, setRoles] = useState([])

  useEffect(() => {
    formRef.resetFields()
    userService.getRoles().then((x) => setRoles(x.data)).catch(() => { })
  }, [formRef])

  const submit = async (values) => {
    let _model = { ...values, id: model?.id }
    setReady(false)
    userService.addOrUpdate(_model)
    .then(() => {
      message.success('User is saved')
      onSuccess()
    }).catch(err => {
      message.error(err.response?.data?.error ?? 'Unexpected error')
    }).finally(() => setReady(true))
  }

  return (
    <Form form={formRef} layout="vertical" onFinish={submit} size="large">
      {model !== null && <Form.Item
        className='IsActiveSwitch IsActiveSwitch-fixed'
        name="isActive"
        label="Aktiflik Durumu:"
        valuePropName="checked"
      >
        <Switch checkedChildren={<CheckOutlined />} unCheckedChildren={<CloseOutlined />} />
      </Form.Item>}
      <Row gutter={16}>
        <Col span={12}>
          <Form.Item
            label="firstName"
            name="firstName"
            rules={[
              { required: true, message: 'Please enter name' },
              { min: 2, message: 'Too short' },
            ]}
            hasFeedback
          >
            <Input placeholder="Ad" />
          </Form.Item>
        </Col>
        <Col span={24}>
          <Form.Item
            label="email"
            name="email"
            rules={[
              { required: true, message: 'Please enter email' },
              { type: 'email', message: 'Invalid email' }
            ]}
            hasFeedback
          >
            <Input type="email" placeholder="name@email.xom" />
          </Form.Item>
        </Col>
        <Col span={24}>
          <Input placeholder='5XX XX XX' />
        </Col>
      </Row>
      {!model && <Password passwordPolicy={passwordPolicy} formRef={formRef} />}
      <Row gutter={16}>
        <Col span={24}>
          <Divider>Roles</Divider>
          <Form.Item name="roles" rules={[{ required: true, message: 'Select role' }]}>
            <Checkbox.Group options={roles?.map((x) => { return { label: x.name, value: x.code } })} />
          </Form.Item>
        </Col>
        <Col span={24}>
          <Button
            type="primary"
            style={{ width: '100%', marginTop: 20 }}
            onClick={() => formRef.submit()}
            disabled={!ready}
            loading={!ready}
          >
            Save
          </Button>
        </Col>
      </Row>
    </Form>
  )
}