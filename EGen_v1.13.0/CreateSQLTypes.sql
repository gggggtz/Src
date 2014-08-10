CREATE RULE unsignedrule as @range > 0
go

create type IDENT_T from numeric(11,0)
go
exec sp_bindrule 'unsignedrule', 'IDENT_T'
create type TRADE_T from numeric(15,0)
go
exec sp_bindrule 'unsignedrule', 'TRADE_T'

create type FIN_AGG_T from numeric(15,2)
go

create type S_PRICE_T from numeric(8,2)
go

create type S_COUNT_T from numeric(15,2)
go
exec sp_bindrule 'unsignedrule', 'S_COUNT_T'

create type S_QTY_T from numeric(6,0)
go

create type BALANCE_T from numeric(12,2)
go

create type VALUE_T from numeric(10,2)
go