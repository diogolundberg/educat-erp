<template>
  <div
    v-show="items && items.length"
    class="bg-red-dim flex p3 mb2 rounded white">
    <div>
      <Icon
        :width="32"
        name="warning2"
        class="mr2" />
    </div>
    <div class="flex-auto">
      <div>O envio dos seus dados gerou pendências:</div>
      <ul>
        <li
          v-for="(item, index) in items"
          :key="index">
          {{ item[messageKey] }}
        </li>
      </ul>
      <div class="bg-white p2 rounded red-dim">
        Após resolver as pendências acima, envie novamente o formulário com
        todos os seus dados corretos.
      </div>
      <div class="mt2">
        Resolveu as pendências e revisou os seus dados?
      </div>
      <Btn
        primary
        yellow
        @click="$emit('click')">
        Enviar novamente para análise
      </Btn>
    </div>
  </div>
</template>

<script>
  export default {
    name: "AlertGroup",
    props: {
      items: {
        type: Array,
        default: () => [],
      },
      messageKey: {
        type: String,
        default: "description",
      },
    },
  };
</script>
