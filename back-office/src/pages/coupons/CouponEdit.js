import React, { useEffect, useState } from 'react'
import { Form, Row, Col, Input, message, Switch, Card, Spin, Button } from 'antd'
import { CloseOutlined, CheckOutlined } from '@ant-design/icons'
import CouponService from './CouponService'

const couponService = new CouponService()

export default function CouponEdit({ onSuccess, model }) {
  const [formRef] = Form.useForm()
  const [ready, setReady] = useState(true)

  useEffect(() => {
    formRef.resetFields()
  }, [formRef])

  const submit = async (values) => {
    let _model = { ...values, id: model?.id }
    setReady(false)
    couponService.addOrUpdate(_model)
      .then(() => {
        message.success('Coupon is saved')
        onSuccess()
      }).catch(err => {
        message.error(err.response?.data?.error ?? 'Unexpected error')
      }).finally(() => setReady(true))
  }

  return (
    <Card>
      <Spin size="large" spinning={!ready} />
      <Form form={formRef} layout="vertical" onFinish={submit} hidden={!ready} size="large">
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
              label="Code"
              name="code"
              rules={[
                { required: true, message: 'Please enter code' },
                { min: 2, message: 'Too short' },
              ]}
              hasFeedback
            >
              <Input placeholder="Code" />
            </Form.Item>
          </Col>
          <Col span={24}>
            <Form.Item
              label="Discount (%)"
              name="discountAmount"
              rules={[
                { required: true, message: 'Please enter discount amount' }
              ]}
              hasFeedback
            >
              <Input type='number' placeholder="Discount" />
            </Form.Item>
          </Col>
          <Col span={24}>
            <Form.Item
              label="Min Amount ($)"
              name="minAmount"
              rules={[{ required: true, message: 'Please enter min amount' }]}
              hasFeedback
            >
              <Input type='number' placeholder="Min Amount" />
            </Form.Item>
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
          </Col>
        </Row>
      </Form>
    </Card>
  )
}