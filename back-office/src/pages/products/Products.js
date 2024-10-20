import React, { useEffect, useState } from 'react'
import { Button, Col, Drawer, Input, message, Row, Table } from 'antd'
import { PlusOutlined, UserOutlined } from '@ant-design/icons'
import Page from '../../components/Page'
import productColumns from './product-columns'
import ProductService from './ProductService'
import ProductEdit from './ProductEdit'

const productService = new ProductService()
const { Search } = Input

export default function Products() {
  const [filter, setFilter] = useState({ orderBy: 'id_asc' })
  const [ready, setReady] = useState(false)
  const [data, setData] = useState()
  const [current, setCurrent] = useState()
  const [drawerVisibility, setDrawerVisibility] = useState(false)

  useEffect(() => {
      load()
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [filter])

  function load() {
    setReady(false)
    productService.getAll(filter)
      .then(m => setData(m.data))
      .catch(() => { message.error("Unexpected error") })
      .finally(() => { setReady(true) })
  }

  function add() {
    setCurrent(null)
    setDrawerVisibility(true)
  }

  async function edit(row) {
    setCurrent(row)
    setDrawerVisibility(true)
  }

  async function remove(row) {
    setReady(false)
    await productService.delete(row.id)
    message.success(`${row.name} is deleted`)
    load()
  }

  function success() {
    setDrawerVisibility(false)
    load()
  }

  return (
    <Page
      title="Products"
      routes={[{ href: '/products', name: 'Products', icon: <UserOutlined /> }]}
      extra={[
        <Button key="add" type="primary" onClick={add}>
          <PlusOutlined /> Add product
        </Button>
      ]}
    >
    <Row gutter={16}>
      <Col>
        <Search
          className="ensearch"
          size="large"
          disabled={!ready}
          allowClear
          enterButton
          onSearch={val => setFilter({ ...filter, pageIndex: 1, search: val === "" ? null : val })}
          key="search"
          placeholder="Search"
        />
      </Col>
    </Row>
      <Table
        rowKey="id"
        scroll={{ x: 1000 }}
        dataSource={data?.result}
        columns={productColumns(edit, remove)}
        onRow={(record, _) => {
          return {
            onDoubleClick: (_) => { edit(record) }
          }
        }}
        loading={!ready}
        pagination={{
          pageSize: filter.pageSize,
          total: data?.rowCount,
          showTotal: (total, _) => `Total of ${total} records`,
          current: filter.pageIndex,
          position: ['bottomCenter'],
          showSizeChanger: true
        }}
        onChange={(f, _, s) => s.order !== undefined
          ? setFilter((p) => ({ ...p, pageIndex: f.current, pageSize: f.pageSize, orderBy: `${s.columnKey}_${s.order}` }))
          : setFilter((p) => ({ ...p, pageIndex: f.current, pageSize: f.pageSize, orderBy: filter.orderBy })
        )}
      />
      <Drawer
        open={drawerVisibility}
        onClose={() => { setDrawerVisibility(false); }}
        width={500}
        styles={{ body: { paddingTop: 20 } }}
        title={current ? 'Edit Product' : 'Add Product'}
      >
        <ProductEdit onSuccess={success} model={current} />
      </Drawer>
    </Page>
  )
}
