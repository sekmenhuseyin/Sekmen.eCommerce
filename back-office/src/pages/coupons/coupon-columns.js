import { Button, Popconfirm } from 'antd'
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';

export default function couponColumns(edit, remove) {
  return [
    {
      title: 'Id',
      width: 120,
      dataIndex: 'id',
    },
    {
      title: 'Code',
      dataIndex: 'code',
    },
    {
      title: 'Discount',
      dataIndex: 'discountAmount',
    },
    {
      title: 'Min Amount',
      dataIndex: 'minAmount',
    },
    {
      title: '',
      align: 'center',
      width: 110,
      fixed: 'right',
      render: (_, row) => (
        <>
          <Button key="edit" type="primary" onClick={() => edit(row)}>
            <EditOutlined />
          </Button>
          <Popconfirm
            key="delete"
            title="Are you sure you want to delete this?"
            onConfirm={() => remove(row)}
            okText="Yes"
            cancelText="No"
          >
            <Button type="danger">
              <DeleteOutlined />
            </Button>
          </Popconfirm>
        </>
      )
    }
  ]
}