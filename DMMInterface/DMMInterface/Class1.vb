Public Interface IDMM
    Sub Close()
    Sub Reset()
    ReadOnly Property ID As String
    ReadOnly Property err As String
    ReadOnly Property DC As IDC
    ReadOnly Property AC As IAC
    ReadOnly Property Resistance As IResistance

    Public Interface IDC
        ReadOnly Property Voltage As IVoltage
        ReadOnly Property Current As ICurrent

        Public Interface IVoltage
            Enum RangeEnum
                AUTO
                _100m
                _1
                _10
                _100
                _1000
            End Enum

            Property Range As RangeEnum

            Function measure() As Double
        End Interface

        Public Interface ICurrent
            Enum RangeEnum
                AUTO
                _10m
                _100m
                _1
                _3
            End Enum
            Property Range As RangeEnum
            Function measure() As Double
        End Interface
    End Interface

    Public Interface IAC
        ReadOnly Property Voltage As IVoltage
        ReadOnly Property Current As ICurrent
        Public Interface IVoltage
            Enum RangeEnum
                AUTO
                _100m
                _1
                _10
                _100
                _1000
            End Enum
            Property Range As RangeEnum
            Function measure() As Double
        End Interface
        Public Interface ICurrent
            Enum RangeEnum
                AUTO
                _1
                _3
            End Enum
            Property Range As RangeEnum
            Function measure() As Double
        End Interface
    End Interface

    Public Interface IResistance
        ReadOnly Property _2W As I2W
        ReadOnly Property _4W As I4W

        Public Interface I2W
            Enum RangeEnum
                AUTO
                _100
                _1K
                _10K
                _100K
                _1M
                _10M
                _100M
            End Enum
            Sub Zero()
            Property Range As RangeEnum
            Function measure() As Double
        End Interface

        Public Interface I4W
            Enum RangeEnum
                AUTO
                _100
                _1K
                _10K
                _100K
                _1M
                _10M
                _100M
            End Enum
            Property Range As RangeEnum
            Function measure() As Double
        End Interface
    End Interface
End Interface
