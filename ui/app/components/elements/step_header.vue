<template>
  <div
    :class="{
      pointer: !disabled,
      'bg-silver': active,
    }"
    class="px2 py3 bg-white shadow1 rounded flex items-center"
    @click="$emit('click')">
    <Ball
      :class="{
        'bg-red-dim': error,
        'bg-yellow green': active && !error,
      }"
      class="mr2">
      {{ index }}
    </Ball>
    <div class="border-right border-silver y3 mr2" />
    <div class="h5 line-height-3 flex-auto">
      <div
        :class="{
          'red-dim': error,
          green: !error,
        }"
        class="bold h3">
        {{ title }}
      </div>
      <div
        :class="{
          'red-dim': error,
          dim: !error,
        }"
        class="h4 mt1">
        {{ description }}
      </div>
    </div>
    <Badge
      v-if="complete"
      success
      big>
      Finalizado
    </Badge>
    <Badge
      v-else-if="error"
      error
      big>
      Erro
    </Badge>
    <Badge
      v-else-if="warning"
      warning
      big>
      Em análise
    </Badge>
    <Badge
      v-else
      big>
      Não preenchido
    </Badge>
  </div>
</template>

<script>
  export default {
    name: "StepHeader",
    props: {
      index: {
        type: [String, Number],
        required: true,
      },
      title: {
        type: String,
        required: true,
      },
      description: {
        type: String,
        required: true,
      },
      disabled: {
        type: Boolean,
        default: false,
      },
      active: {
        type: Boolean,
        default: false,
      },
      complete: {
        type: Boolean,
        default: false,
      },
      error: {
        type: Boolean,
        default: false,
      },
      warning: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>
