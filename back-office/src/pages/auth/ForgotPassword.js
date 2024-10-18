import React, { useState } from 'react'
import { useNavigate, Link } from 'react-router-dom'
import { Card, Form, Row, Col, Input, Button, message } from 'antd'
import LoginContainer from './LoginContainer'
import AuthService from './AuthService'

const authService = new AuthService()

export default function ForgotPassword() {
  const navigate = useNavigate()
  const [formRef] = Form.useForm()
  const [ready, setReady] = useState(true)

  const submit = async (model) => {
    setReady(false)
    authService.forgotPassword(model)
      .then((x) => {
        message.success('Please check your email')
        navigate({ pathname: '/login', state: { email: model.email } })
      }).catch(err => {
        message.error(err.response?.data?.errors ?? 'Unexpected error')
      }).finally((x) => setReady(true))
  }

  return (
    <LoginContainer>
      <Card>
        <Form
          form={formRef}
          layout="vertical"
          onFinish={submit}
          size="large"
          requiredMark={false}
        >
          <Row gutter={[16, 0]}>
            <Col span={24}>
              <Form.Item
                label="Email"
                name="email"
                rules={[
                  { required: true, message: 'Write your email' },
                  { type: 'email', message: 'Invalid email' },
                ]}
                hasFeedback
              >
                <Input type="email" />
              </Form.Item>
            </Col>
            <Col span={24}>
              <Button
                type="primary"
                htmlType="submit"
                style={{ width: '100%', marginTop: 20 }}
                onClick={() => formRef.submit()}
                disabled={!ready}
                loading={!ready}
              >
                Send Email
              </Button>
            </Col>
            <Col span={24} style={{ textAlign: 'center', marginTop: 10 }}>
              <Link to="/login">
                Login</Link>
            </Col>
          </Row>
        </Form>
      </Card>
    </LoginContainer>
  )
}
