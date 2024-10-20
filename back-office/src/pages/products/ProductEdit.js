import React, { useEffect, useState } from 'react'
import { Form, Row, Col, Input, message, Switch, Card, Spin, Button } from 'antd'
import { CloseOutlined, CheckOutlined } from '@ant-design/icons'
import ProductService from './ProductService'

const productService = new ProductService()

export default function ProductEdit({ onSuccess, model }) {
  const [formRef] = Form.useForm()
  const [ready, setReady] = useState(true)

  useEffect(() => {
    formRef.resetFields()
  }, [formRef])

  const submit = async (values) => {
    let _model = { ...values, id: model?.id }
    setReady(false)
    productService.addOrUpdate(_model)
      .then(() => {
        message.success('Product is saved')
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
              label="Price ($)"
              name="price"
              rules={[{ required: true, message: 'Please enter price' }]}
              hasFeedback
            >
              <Input type='number' placeholder="Price" />
            </Form.Item>
          </Col>
          <Col span={24}>
            <Form.Item
              label="Description"
              name="description"
              rules={[
                { required: true, message: 'Please enter description' },
                { min: 2, message: 'Too short' },
              ]}
              hasFeedback
            >
              <Input placeholder="Description" />
            </Form.Item>
          </Col>
          <Col span={24}>
            <Form.Item
              label="CategoryName"
              name="categoryName"
              rules={[
                { required: true, message: 'Please enter categoryName' },
                { min: 2, message: 'Too short' },
              ]}
              hasFeedback
            >
              <Input placeholder="CategoryName" />
            </Form.Item>
          </Col>
          <Col span={24}>
            <Form.Item
              label="ImageUrl"
              name="imageUrl"
              rules={[
                { required: true, message: 'Please enter imageUrl' },
                { min: 2, message: 'Too short' },
              ]}
              hasFeedback
            >
              <Input placeholder="ImageUrl" />
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
    </Card >
  )
}