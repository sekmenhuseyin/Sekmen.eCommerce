import { Button, Result } from 'antd'
import { useNavigate } from 'react-router'

export default function Page403() {
  const navigate = useNavigate()

  return (
    <Result
      status="403"
      title="Yetkisiz girişim"
      subTitle="Lütfen ulaşmak istediğiniz sayfa için yöneticinize başvurun"
      extra={[
        <Button key={"button"} type="primary" onClick={() => navigate('/')}>
          Dashboard'a git
        </Button>
      ]}
    ></Result>
  )
}
