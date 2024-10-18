import React from 'react'
import { Row, Col } from 'antd'

export default function LoginContainer({ children }) {
  return (
    <div className='LoginContainer'>
      <div className='MidContainer'>
        {children}
      </div>
      <Row>
        <Col span={24}>
          <p className='BottomText'>
            ©{new Date().getFullYear()} Created by sekmen.dev
          </p>
        </Col>
      </Row>
    </div>
  )
}