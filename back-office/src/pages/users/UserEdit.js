import React, { useEffect, useState } from 'react'
import { Form, Row, Col, Input, Button, message, Switch, Select, Card, Spin } from 'antd'
import { CloseOutlined, CheckOutlined } from '@ant-design/icons'
import UserService from './UserService'
import { Password } from '../../components/Password'

const userService = new UserService()
const { Option } = Select

export default function UserEdit({ onSuccess, roles, model, passwordPolicy = {} }) {
  const [formRef] = Form.useForm()
  const [ready, setReady] = useState(true)

  useEffect(() => {
    formRef.resetFields()
    if (model)
      userService.getRole(model.id).then((x) => formRef.setFieldValue("role", x.data.value)).catch(() => { })
  }, [formRef, model])

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
    <Card>
      <Spin size="large" spinning={!ready} />
      <Form form={formRef} initialValues={model} layout="vertical" onFinish={submit} hidden={!ready} size="large">
        {model !== null && <Form.Item
          className='IsActiveSwitch IsActiveSwitch-fixed'
          name="isActive"
          label="Aktiflik Durumu:"
          valuePropName="checked"
        >
          <Switch checkedChildren={<CheckOutlined />} unCheckedChildren={<CloseOutlined />} />
        </Form.Item>}
        <Row gutter={16}>
          <Col span={24}>
            <Form.Item
              label="Name"
              name="name"
              rules={[
                { required: true, message: 'Please enter name' },
                { min: 2, message: 'Too short' },
              ]}
              hasFeedback
            >
              <Input placeholder="Name" />
            </Form.Item>
          </Col>
          <Col span={24}>
            <Form.Item
              label="Email"
              name="email"
              rules={[
                { required: true, message: 'Please enter email' },
                { type: 'email', message: 'Invalid email' }
              ]}
              hasFeedback
            >
              <Input type="email" placeholder="Email" disabled={model} />
            </Form.Item>
          </Col>
          <Col span={24}>

            <Form.Item
              label="Phone"
              name="phoneNumber"
              rules={[{ required: true, message: 'Please enter phone number' }]}
              hasFeedback
            >
              <Input placeholder="Phone" />
            </Form.Item>
          </Col>
        </Row>
        {!model && <Password passwordPolicy={passwordPolicy} formRef={formRef} />}
        <Row gutter={16}>
          <Col span={24}>
            <Form.Item
              label="Role"
              name="role"
              rules={[{ required: true, message: 'Select role' }]}
            >
              <Select allowClear>
                {roles.map((item) => <Option key={item} value={item}>{item}</Option>)}
              </Select>
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
    </Card>
  )
}