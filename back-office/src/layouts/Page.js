import React from 'react'
import { PageHeader } from '@ant-design/pro-layout'
import { HomeOutlined } from '@ant-design/icons'

function Page({ title, subTitle, routes, children, extra }) {
  routes = routes || []

  return (
    <PageHeader
      title={title}
      subTitle={subTitle}
      extra={extra}
      breadcrumb={{
        items: [
          {
            href: '/',
            name: 'Home',
            icon: <HomeOutlined />
          },
          ...routes,
        ],
        seperator: '>',
        itemRender: renderBreadcrumbItem
      }}
    >
      {children}
    </PageHeader>
  )

  function renderBreadcrumbItem (item) {
    return item.icon ? (
      <>
        {item.icon}
        <span> {item.name}</span>
      </>
    ) : (
        item.name
    )
  }
}

export default Page