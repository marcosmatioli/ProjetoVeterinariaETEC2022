create database bdClinicaVeterinaria;
use bdClinicaVeterinaria;
#drop database bdClinicaVeterinaria;

create table tbCliente(
codCliente int primary key AUTO_INCREMENT,
nomeCliente varchar(50),
telefoneCliente varchar(50),
enderecoCliente varchar(50),
cepCliente varchar(10),
usuario varchar(50),
senha varchar(50),
tipoUsuario int
);

#tipoUsuario 1 é ADM / 2 USUARIO
create table tbTipoAnimal(
codTipoAnimal int primary key AUTO_INCREMENT,
tipoAnimal varchar(50)
);
create table tbAnimal(
codAnimal int primary key AUTO_INCREMENT,
nomeAnimal varchar(50),
fotoAnimal varchar(100),
codTipoAnimal int,
codCliente int,
CONSTRAINT foreign key(codCliente) references tbCliente(codCliente),
CONSTRAINT foreign key(codTipoAnimal) references tbTipoAnimal(codTipoAnimal)
);
create table tbVeterinario(
codVet int primary key AUTO_INCREMENT,
nomeVet varchar(100)
);
create table tbAgendamento(
codAgendamento int primary key AUTO_INCREMENT,
dataAgendamento varchar(100),
horaAgendamento varchar(100),
Reclamacao varchar(100),
codAnimal int,
codVet int,
codCliente int,
constraint foreign key(codAnimal) references tbAnimal(codAnimal),
constraint foreign key(codVet) references tbVeterinario(codVet)
);
create table tbAtendimento(
codAtendimento int primary key AUTO_INCREMENT,
dataAtendimento varchar(100),
horaAtendimento varchar(100),
Diagnostico varchar(100),
codAnimal int,
codVet int,
codCliente int,
constraint foreign key(codAnimal) references tbAnimal(codAnimal),
constraint foreign key(codVet) references tbVeterinario(codVet)
);
#drop table tbAtendimento;
#drop table tbAgendamento;
#selects
#select * from tbCliente;
#select * from tbAtendimento;
#select * from tbAnimal;
#select * from tbTipoAnimal;
#select * from tbVeterinario;
#select * from tbAgendamento;

#Criação das views para o consultar grid
create view vwAtendimento as select * from tbAtendimento;
create view vwAgendamento as select * from tbAgendamento;
create view vwCliente as select * from tbCliente;
create view vwVeterinario as select * from tbVeterinario;
create view vwTipoAnimal as select * from tbTipoAnimal;
create view vwAnimal as select tbAnimal.codAnimal,tbAnimal.nomeAnimal,tbAnimal.fotoAnimal,tbTipoAnimal.TipoAnimal,tbCliente.nomeCliente from tbAnimal inner join tbCliente on tbCliente.codCliente = tbAnimal.codCliente inner join tbTipoAnimal on tbTipoAnimal.codTipoAnimal = tbAnimal.codTipoAnimal;

#inserir usuario
insert into tbCliente(codCliente,nomeCliente,telefoneCliente,enderecoCliente,cepCliente,usuario,senha,tipoUsuario) values (1,"ADM","00 00000-0000","rua adolfino","000000-000","adm","adm",1);
insert into tbCliente(codCliente,nomeCliente,telefoneCliente,enderecoCliente,cepCliente,usuario,senha,tipoUsuario) values (2,"usuario","00 00000-0000","rua adolfino", "000000-000","usuario","usuario",2);

#select para testar o usuario, primeiro testar aqui para depois levar para o codigo
select usuario,senha,tipoUsuario from tbCliente where usuario = "adm" and senha = "adm" and tipoUsuario = 1;
select * from tbCliente;
#inserir tipo de animal
insert into tbTipoAnimal(codTipoAnimal,TipoAnimal) values (1,"Roedor");
insert into tbTipoAnimal(codTipoAnimal,TipoAnimal) values (2,"Papagaio");
insert into tbTipoAnimal(codTipoAnimal,TipoAnimal) values (3,"Peixe");
insert into tbTipoAnimal(codTipoAnimal,TipoAnimal) values (4,"Gato");
insert into tbTipoAnimal(codTipoAnimal,TipoAnimal) values (5,"Cachorro");
insert into tbTipoAnimal(codTipoAnimal,TipoAnimal) values (6,"Piriquito");

#inserir veterinario
insert into tbVeterinario(codVet,nomeVet) values (1,"Alex Karev");
insert into tbVeterinario(codVet,nomeVet) values (2,"Cristina Yang");
insert into tbVeterinario(codVet,nomeVet) values (3,"Derek Sheppard");
insert into tbVeterinario(codVet,nomeVet) values (4,"Meredith Grey");
insert into tbVeterinario(codVet,nomeVet) values (5,"Owen Hunt");
insert into tbVeterinario(codVet,nomeVet) values (6,"Lexy Grey");
insert into tbVeterinario(codVet,nomeVet) values (7,"Mark Sloan");

#select para aparecer os animais do usuario logado, consegui fazer o inner join que traz os animais do 
#usuario em questão agora só alinhar os dados que eu quero
#trazer para usar no ASP, mudar no where para colocar qual o usuario em questão que vai mostrar os animais dele
-- select tbAnimal.codAnimal,tbAnimal.nomeAnimal,tbAnimal.fotoAnimal,tbAnimal.codTipoAnimal,tbAnimal.codCliente,tbCliente.nomeCliente from tbAnimal inner join tbCliente on tbAnimal.codCliente = tbCliente.codCliente where tbCliente.nomeCliente = "ADM";

#update no cliente, teste para ver se iria funcionar
-- update tbCliente set nomeCliente="user",telefoneCliente="88 88888-8888",cepCliente="55555-555",enderecoCliente="Rua Dronsfiled numero 30",usuario="usuario",senha="usuario",TipoUsuario=2 where codCliente=2;

#select da tabela agendamento
-- select * from tbAgendamento;

#select para trazer todos os agendamentos de um cliente com base no codCliente
-- select tbAgendamento.codAgendamento,tbAgendamento.dataAgendamento,tbAgendamento.horaAgendamento,tbAgendamento.Reclamacao,tbVeterinario.nomeVet,tbAgendamento.codVet,tbAnimal.nomeAnimal,tbCliente.nomeCliente from tbAgendamento
-- inner join tbAnimal on tbAnimal.codAnimal = tbAgendamento.codAnimal inner join tbVeterinario on tbAgendamento.codVet = tbVeterinario.codVet inner join tbCliente on tbCliente.codCliente = tbAgendamento.codCliente where tbAgendamento.codCliente = 1;

#select que traz todos os agendamentos
-- select tbAgendamento.codAgendamento,tbAgendamento.dataAgendamento,tbAgendamento.horaAgendamento,tbAgendamento.Reclamacao,tbVeterinario.nomeVet,tbAgendamento.codVet,tbAnimal.nomeAnimal,tbCliente.nomeCliente from tbAgendamento  
-- inner join tbAnimal on tbAnimal.codAnimal = tbAgendamento.codAnimal inner join tbVeterinario on tbAgendamento.codVet = tbVeterinario.codVet inner join tbCliente on tbAgendamento.codCliente = tbCliente.codCliente;

#select para trazer todos os atendimentos de um cliente com base no codCliente
-- select tbAtendimento.codAtendimento,tbAtendimento.dataAtendimento,tbAtendimento.horaAtendimento,tbAtendimento.Diagnostico,tbVeterinario.nomeVet,tbAtendimento.codVet,tbAnimal.nomeAnimal,tbCliente.nomeCliente from tbAtendimento inner join tbAnimal on tbAnimal.codAnimal = tbAtendimento.codAnimal inner join tbVeterinario on tbAtendimento.codVet = tbVeterinario.codVet inner join tbCliente on tbAtendimento.codCliente = tbCliente.codCliente where tbAtendimento.codCliente = 1;

#select que traz todos os atendimentos
-- select tbAtendimento.codAtendimento,tbAtendimento.dataAtendimento,tbAtendimento.horaAtendimento,tbAtendimento.Diagnostico,tbVeterinario.nomeVet,tbAtendimento.codVet,tbAnimal.nomeAnimal,tbCliente.nomeCliente from tbAtendimento inner join tbAnimal on tbAnimal.codAnimal = tbAtendimento.codAnimal inner join tbVeterinario on tbAtendimento.codVet = tbVeterinario.codVet inner join tbCliente on tbAtendimento.codCliente = tbCliente.codCliente;

#select para trazer animal e nome cliente
-- select tbAnimal.nomeAnimal,tbCliente.nomeCliente from tbAnimal inner join tbCliente on tbAnimal.codAnimal = tbCliente.codCliente;

#teste concat para trazer em uma tabela o nome do usuario e o pet junto e tinha que ter uma tabela separada do codigo do animal ( isso aqui demorou umas 3 horinhas humilde pra entender e fazer )
-- select tbAnimal.codAnimal,CONCAT('Dono: ',nomeCliente,' | Pet: ', nomeAnimal) from tbCliente inner join tbAnimal on tbAnimal.codCliente = tbCliente.codCliente;