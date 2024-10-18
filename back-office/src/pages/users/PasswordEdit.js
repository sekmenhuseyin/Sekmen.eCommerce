import React, { useEffect, useState } from 'react'
import { Form, Row, Col, Input, Button, message } from 'antd'
import UserService from './UserService'
import { Password } from '../../components/Password'

const userService = new UserService()

export default function PasswordEdit({ onSuccess }) {
  const [formRef] = Form.useForm()
  const [ready, setReady] = useState(true)
  const [passwordPolicy, setPasswordPolicy] = useState({})

  useEffect(() => {
    userService.getPasswordPolicy().then((x) => {
      setPasswordPolicy(x.data)
    }).catch(() => { })
    formRef.resetFields()
  }, [formRef])

  const submit = (values) => {
    setReady(false)
    userService.updatePassword(values).then(() => {
      message.success('Your password has changed')
      formRef.resetFields()
      onSuccess()
    }).catch((err) => {
      message.error(err.response?.data?.error ?? 'Unexpected error')
    }).finally(() => setReady(true))
  }

  return (
    <Form form={formRef} layout="vertical" onFinish={submit} size="large">
      <Row gutter={[16, 0]}>
        <Col span={24}>
          <Form.Item
            label="Current password"
            name="oldPassword"
            rules={[
              { required: true, message: 'Please enter your password' }
            ]}
            hasFeedback
          >
            <Input.Password />
          </Form.Item>
        </Col>
      </Row>
      <Password passwordPolicy={passwordPolicy} formRef={formRef} showRepeat={true} />
      <Row gutter={[16, 0]}>
        <Col span={24}>
          <Button
            type="primary"
            style={{ width: '100%', marginTop: 20 }}
            onClick={() => formRef.submit()}
            disabled={!ready}
            loading={!ready}
          >
            Kaydet
          </Button>
        </Col>
      </Row>
    </Form>
  )
}
