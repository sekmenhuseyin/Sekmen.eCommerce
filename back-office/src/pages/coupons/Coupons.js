import React, { useEffect, useState } from 'react'
import { Button, Col, Drawer, Input, message, Row, Table } from 'antd'
import { PlusOutlined, UserOutlined } from '@ant-design/icons'
import Page from '../../components/Page'
import couponColumns from './coupon-columns'
import CouponService from './CouponService'
import CouponEdit from './CouponEdit'

const couponService = new CouponService()
const { Search } = Input

export default function Coupons() {
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
    couponService.getAll(filter)
      .then(m => setData(m.data.value))
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
    await couponService.delete(row.id)
    message.success(`${row.name} is deleted`)
    load()
  }

  function success() {
    setDrawerVisibility(false)
    load()
  }

  return (
    <Page
      title="Coupons"
      routes={[{ href: '/coupons', name: 'Coupons', icon: <UserOutlined /> }]}
      extra={[
        <Button key="add" type="primary" onClick={add}>
          <PlusOutlined /> Add coupon
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
        columns={couponColumns(edit, remove)}
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
        title={current ? 'Edit Coupon' : 'Add Coupon'}
      >
        <CouponEdit onSuccess={success} model={current} />
      </Drawer>
    </Page>
  )
}
