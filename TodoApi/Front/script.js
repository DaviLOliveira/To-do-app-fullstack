const API_BASE_URL = 'https://localhost:7286/api';


let todos = [];

const formNovaTarefa = document.querySelector('#form-nova-tarefa');
const inputNovaTarefa = document.querySelector('#form-nova-tarefa');
const listaTarefas = document.querySelector('#lista-tarefas');

formNovaTarefa.addEventListener('submit', (evento) => {

    evento.preventDefault();

    const textoDaTarefa = inputNovaTarefa.value;

    console.log('Texto digitado', textoDaTarefa);

    if (textoDaTarefa.trim() === '') {
        alert('Por favor, digite uma tarefa.');
        return;
    }

    inputNovaTarefa.value = '';
}

async function fetchTodoAPI() {
    console.log('Buscando tarefas na API...');
}

async function addTodoAPI(textoDaTarefa) {
    console.log('Renderizando a lista de tarefas....');
}

function renderTodos() {
    console.log('Renderizando lista de tarefas...');
}

function handleFormSubmit(evento) {
    console.log('Formulário enviado!')
}

document.addEventListener('DOMContentLoaded', () => {
    console.log('Página carregada. Aplicação Iniciada.')

    fetchTodos();
});
console.log('Aplicação inciada!');
