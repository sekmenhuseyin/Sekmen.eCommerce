import { CheckCircleOutlined, CloseCircleOutlined } from "@ant-design/icons";
import { Button, Col, Form, Input, Row } from "antd";
import { useState } from "react";
import { createRandomPassword, PasswordCharacterCount } from "../utils/functions";

export function Password({ formRef, passwordPolicy, showRepeat = false }) {
  const [passwordCharacterCounts, setPasswordCharacterCounts] = useState(PasswordCharacterCount(""))

  const createPassword = () => {
    var password = createRandomPassword(passwordPolicy)
    setPasswordCharacterCounts(PasswordCharacterCount(password))
    formRef.setFieldsValue({ password: password })
    if (showRepeat)
      formRef.setFieldsValue({ repeatPassword: password })
  }

  const TestPassword = (e) => {
    let password = e.target.value
    setPasswordCharacterCounts(PasswordCharacterCount(password))
  }

  function renderIcon(isTrue, text) {
    return <p className={`isActive small isActive-${isTrue ? "true" : "false"}`}>
      {(isTrue ? <CheckCircleOutlined /> : <CloseCircleOutlined />)}
      {text}
    </p>
  }

  return (<>
    <Row style={{ margin: "20px 0" }}>
      <Col span={24}>
        <Form.Item
          name="password"
          className="content-label"
          label={<>
            <span>New Password</span>
            <Button className="btn-link-right" type="link" onClick={() => createPassword()}>Create</Button>
          </>}
          rules={[{ required: true, message: 'Please write a password' }]}
          hasFeedback
        >
          <Input onChange={TestPassword} />
        </Form.Item>
      </Col>
      <Col span={24} className='password-options'>
        {passwordPolicy.requiredLength && renderIcon(passwordCharacterCounts.length >= passwordPolicy.requiredLength, `Password must be ${passwordPolicy.requiredLength} character`)}
        {passwordPolicy.requiredLength && renderIcon(passwordCharacterCounts.length > 0 && !formRef.getFieldValue("password")?.includes(' '), 'Can not have space')}
        {passwordPolicy.requireUppercase && renderIcon(passwordCharacterCounts.upperCaseCount >= passwordPolicy.requiredUniqueChars, `Password must have ${passwordPolicy.requiredUniqueChars} capital`)}
        {passwordPolicy.requireLowercase && renderIcon(passwordCharacterCounts.lowerCaseCount >= passwordPolicy.requiredUniqueChars, `Password must have ${passwordPolicy.requiredUniqueChars} lowercase`)}
        {passwordPolicy.requireDigit && renderIcon(passwordCharacterCounts.numberCount >= passwordPolicy.requiredUniqueChars, `Password must have ${passwordPolicy.requiredUniqueChars} number`)}
        {passwordPolicy.requireNonAlphanumeric && renderIcon(passwordCharacterCounts.specialCharacterCount >= passwordPolicy.requiredUniqueChars, `Password must have ${passwordPolicy.requiredUniqueChars} symbol`)}
        {passwordPolicy.changePeriodInDays && <p className={`isActive small`}>Must change in {passwordPolicy.changePeriodInDays} days</p>}
        {passwordPolicy.lastPasswordCount && <p className={`isActive small`}>Must not be same as {passwordPolicy.lastPasswordCount} old passwords</p>}
      </Col>
    </Row>
    {showRepeat && <Row gutter={[16, 0]}>
      <Col span={24}>
        <Form.Item
          label="New password again"
          name="repeatPassword"
          rules={[
            { required: true, message: 'Please write password again' },
            ({ getFieldValue }) => ({
              validator(_, value) {
                if (!value || getFieldValue('password') === value)
                  return Promise.resolve()
                return Promise.reject('Passwords must be same')
              },
            }),
          ]}
          dependencies={['password']}
          hasFeedback
        >
          <Input.Password />
        </Form.Item>
      </Col>
    </Row>}
  </>)
}