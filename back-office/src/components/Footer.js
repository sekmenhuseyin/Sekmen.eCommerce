import { Layout } from 'antd'

const { Footer: AntFooter } = Layout

export default function Footer() {
  return (
    <AntFooter style={{ textAlign: 'center' }}>
      ©{new Date().getFullYear()} Created by sekmen.dev
    </AntFooter>
  )
}
