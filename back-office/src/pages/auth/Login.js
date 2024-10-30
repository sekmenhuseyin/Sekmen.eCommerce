import { jwtDecode } from 'jwt-decode'
import React, { useState } from 'react'
import { useNavigate, Link } from 'react-router-dom'
import { Col, Form, Input, Button, Row, Card, message } from 'antd'
import AuthService from './AuthService'
import LoginContainer from './LoginContainer'
import useLocalStorage from '../../utils/useLocalStorage'

const uathService = new AuthService()

export default function Login({ location }) {
  const navigate = useNavigate()
  const formState = location?.state
  const [formRef] = Form.useForm()
  const [, setValue] = useLocalStorage()
  const [ready, setReady] = useState(true)

  async function otpSuccess(model) {
    let jwt = jwtDecode(model.token)
    if (jwt.role !== "ADMIN") {
      message.error("You are not authorized")
      return
    }

    message.success(`Welcome ${model.user.name}`)
    setValue({ access_token: model.token })
    await new Promise(resolve => setTimeout(resolve, 1000))

    if (model.user.mustChangePassword) {
      navigate('/change-password')
    }
    else {
      navigate('/')
    }
  }

  const login = async (model) => {
    setReady(false)
    uathService
      .login(model)
      .then((response) => {
        if (response.status === 200 && response.data && response.data.value)
          otpSuccess(response.data.value)
        else
          message.error("Giriş başarısız oldu")
      })
      .catch((err) => {
        message.error(err.response?.data?.errors ?? 'Unexpected error')
      }).finally((x) => setReady(true))
  }
  return (
    <LoginContainer>
      <Card style={{ marginBottom: 10, marginTop: 20, width: '100%', maxWidth: 400 }}>
        <Form
          form={formRef}
          layout="vertical"
          size="large"
          onFinish={login}
          initialValues={formState}
          requiredMark={false}
        >
          <Row gutter={[8, 0]}>
            <Col span={24}>
              <Form.Item
                label="Email"
                name="email"
                rules={[
                  { required: true, message: 'Write your email' },
                  { type: 'email', message: 'Invalid email' }
                ]}
              >
                <Input />
              </Form.Item>
            </Col>
            <Col span={24}>
              <Form.Item
                label="Parola"
                name="password"
                rules={[
                  { required: true, message: 'Write your password' }
                ]}
              >
                <Input.Password />
              </Form.Item>
            </Col>
            <Col span={24} style={{ paddingTop: 10 }}>
              <Link style={{ float: 'right' }} to="/forgot-password">
                Forgot Password
              </Link>
            </Col>
            <Col span={24}>
              <Button
                htmlType="submit"
                type="primary"
                style={{ width: '100%', marginTop: 20 }}
                onClick={() => formRef.submit()}
                disabled={!ready}
                loading={!ready}
              >
                Login
              </Button>
            </Col>
          </Row>
        </Form>
      </Card>
    </LoginContainer>
  )
}
