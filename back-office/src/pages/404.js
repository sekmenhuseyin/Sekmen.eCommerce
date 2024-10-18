import { Button, Result } from 'antd'
import { useNavigate } from 'react-router'

export default function Page404() {
  const navigate = useNavigate()
  
  return (
    <Result
      status="404"
      title="Sayfa bulunamadı"
      subTitle="Lütfen ulaşmak istediğiniz sayfa için sol menüyü kullanın"
      extra={[
        <Button key={"button"} type="primary" onClick={() => navigate('/')}>
          Dashboard'a git
        </Button>
      ]}
    ></Result>
  )
}
